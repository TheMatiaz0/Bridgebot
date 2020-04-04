using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    public Hp Hp { get; private set; }

    [SerializeField]
    private uint startMaxHp = 10;

    InputActions inputActions;
    private Transform curShotPoint;

    [SerializeField, Button("↺", Method = nameof(SetDefCamera))]
    private Camera cam;

    private Direction currentDirection = Direction.Right;
    private bool vIsChanged = false;
    public bool Moving { get; private set; }
    private Vector2 movementInput = Vector2.zero;
    public bool LockMovement { get; set; }

    [SerializeField]
    private FreezeMenu pauseMenu;

    [SerializeField]
    private FreezeMenu gameOver = null;

    private void SetDefCamera()
    {
        cam = Camera.main;
    }

    protected override void Awake()
    {
        base.Awake();
        SetPistolVisible(false);
        inputActions = new InputActions();

        Hp = new Hp(startMaxHp, 0, startMaxHp);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HpableExtension.IsFromWrongTeam(this, collision, out Bullet bullet))
        {
            this.Hp.TakeHp(bullet.Dmg, "Bullet");
            bullet.Kill();
        }
    }

    protected void OnEnable()
    {
        inputActions.Enable();
    }

    protected void OnDisable()
    {
        inputActions.Disable();
    }
    public void SetPistolVisible(bool val)
    {
        gunObj.gameObject.SetActive(val);
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

        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
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
        return (Vector2)cam.ScreenToWorldPoint(Input.mousePosition) - gunObj.transform.Get2DPos();
    }

    private void OnShoot()
    {
        if (LockMovement)
        {
            return;
        }

        Equipment.Instance.GetCurrent()?.OnUse();
    }

    private void OnPause ()
    {
        pauseMenu.EnableMenuWithPause(!pauseMenu.IsPaused);
    }

    public void LaunchGameOver ()
    {
        gameOver.EnableMenuWithPause(true);
    }

}
