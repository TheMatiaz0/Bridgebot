using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : AutoInstanceBehaviour<Player>, IHpable
{
    [SerializeField, RequiresAny]
    private Transform leftShotPoint, rightShotPoint, leftPistolPos, rightPistolPos;

    [field: Range(1, 10), SerializeField] public float Speed { get; set; } = 1;

    [SerializeField]
    private GunRepresent gunObj;

    [Auto]
    public Animator Animator { get; private set; }

    [Auto]
    public SpriteRenderer Renderer { get; private set; }
    [Auto]
    public Rigidbody2D Rigidbody2D { get; private set; }

    public Team CurrentTeam { get; private set; } = Team.Good;
    bool end = false;
    public Hp Hp { get; private set; }

    [SerializeField]
    private uint startMaxHp = 10;

    [SerializeField]
    private HpManager hpManager = null;

    InputActions inputActions;
    private Transform curShotPoint;

    [SerializeField, Button("↺", Method = nameof(SetDefCamera))]
    private Camera cam;

    private Direction currentDirection = Direction.Right;
    private bool vIsChanged = false;
    public bool Moving { get; private set; }
    private Vector2 movementInput = Vector2.zero;
    public bool LockMovement { get; set; }

    public IslandEnterTrigger LastIsland { get; set; } = null;

    [SerializeField]
    private FreezeMenu pauseMenu;

    [SerializeField]
    private FreezeMenu gameOver = null;

    [SerializeField]
    private BridgeSelection selection = null;

    private Vector2 lookPosition;

    private Direction lastDirection = Direction.Right;
    private ItemOnTrigger itemTrigger = null;
    public event EventHandler OnCorrectEnds = delegate { };
    private void SetDefCamera()
    {
        cam = Camera.main;
    }

    protected override void Awake()
    {
        base.Awake();
        SetPistolVisible(false);
        inputActions = new InputActions();
        inputActions.Player.Rotation.performed += ctx => lookPosition = ctx.ReadValue<Vector2>();

        Hp = new Hp(startMaxHp, 0, startMaxHp);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (HpableExtension.IsFromWrongTeam(this, collision, out Bullet bullet))
        {
            this.Hp.TakeHp(bullet.Dmg, "Bullet");
            bullet.Kill();
        }

        if (itemTrigger = collision.GetComponent<ItemOnTrigger>())
        {
            return;
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<ItemOnTrigger>())
            itemTrigger = null;
    }

    protected void Start()
    {
        Hp = new Hp(startMaxHp, 0, startMaxHp);
        hpManager.CurHealth = Hp;
        Hp.OnValueChangeToMin += Hp_OnValueChangeToMin;
        hpManager.Refresh();
    }

    private void Hp_OnValueChangeToMin(object sender, Hp.HpChangedArgs e)
    {
        LaunchGameOver();
    }

    protected void OnEnable()
    {
        inputActions?.Enable();
    }

    protected void OnDisable()
    {
        inputActions?.Disable();
    }
    public void SetPistolVisible(bool val)
    {
        gunObj?.gameObject?.SetActive(val);
    }
    private void Update()
    {
        GunUpdate();
        InputUpdate();
    }

    private void GunUpdate()
    {
        if (LockMovement)
        {
            return;
        }

        Vector2 mousePos;

        if (Gamepad.current != null)
        {
            if (lookPosition == Vector2.zero)
            {
                lookPosition = lastDirection;
            }

            lastDirection = lookPosition.ToDirection();

            mousePos = lookPosition + this.transform.Get2DPos();
        }

        else
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        gunObj.transform.LookAt2D(mousePos);

        gunObj.Renderer.flipY = ((Vector2)this.transform.position).x > mousePos.x;
        curShotPoint = gunObj.Renderer.flipY ? leftShotPoint : rightShotPoint;

        if (movementInput == Vector2.right)
        {
            this.Renderer.flipX = true;
        }
        else if (movementInput == Vector2.left)
        {
            this.Renderer.flipX = false;
        }
    }

    private void InputUpdate()
    {
        if (vIsChanged)
        {
            this.Rigidbody2D.velocity = Vector2.zero;
            vIsChanged = false;
        }

        if (LockMovement)
        {
            Animator.SetBool("Move", false);
            return;
        }

        Direction current = Direction.Empty;

        Move(movementInput);
        bool hasMoved;
        if (hasMoved = (current != Direction.Empty))
        {
            this.currentDirection = current;
        }

        Moving = hasMoved;

        Animator.SetBool("Move", hasMoved);

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        void Move(Direction dir)
        {
            this.Rigidbody2D.velocity += dir.ToVector2() * Speed;
            current += dir;

            vIsChanged = true;
        }
    }

    private void OnMovement(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    public Vector2 GetFrom()
    {
        return curShotPoint.position;
    }
    public Direction GetLookDirection()
    {
        if (Gamepad.current != null)
        {
            if (lookPosition == Vector2.zero)
            {
                lookPosition = lastDirection;
            }

            lastDirection = lookPosition.ToDirection();

            return (Vector2)(lookPosition);
        }

        else
        {
            return ((Vector2)(Camera.main.ScreenToWorldPoint(
            Input.mousePosition) - this.gunObj.transform.position))
            .ToDirection();
        }
    }

    private void OnShoot()
    {
        if (LockMovement)
        {
            return;
        }

        Equipment.Instance?.GetCurrent()?.OnUse();
    }

    private void OnPause()
    {
        pauseMenu?.EnableMenuWithPause(!pauseMenu.IsPaused);
    }

    private void OnInteraction()
    {
        LastIsland?.Interaction();
        itemTrigger?.Interaction();
    }


    public void LaunchGameOver()
    {
        if (end)
            return;
        end = true;
        OnCorrectEnds(this, EventArgs.Empty);
        Invoke((_) => gameOver?.EnableMenuWithPause(true), 0.2f);

        LockMovement = true;
    }

    private void OnConfirmSelection()
    {
        selection?.ConfirmSelection();
    }

    private void OnCancelSelection()
    {
        selection?.CancelSelection();
    }
}
