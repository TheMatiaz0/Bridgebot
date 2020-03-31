using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : AutoInstanceBehaviour<Player>, IHpable
{
    [SerializeField]
    private SerializedTimeSpan jumpDelay = TimeSpan.FromSeconds(1);
    [SerializeField, ResetButton(1)]
    private float MoveSize = 1;
    private Cyberevolver.Unity.CooldownController moveCooldown;
    [SerializeField, RequiresAny]
    private Transform leftShotPoint, rightShotPoint, leftPistolPos, rightPistolPos;
    [SerializeField]
    private GunRepresent gunObj;
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

    private void SetDefCamera()
    {
        cam = Camera.main;
    }

    protected override void Awake()
    {
        base.Awake();
        SetPistolVisibile(false);
        inputActions = new InputActions();

        Hp = new Hp(startMaxHp, 0, startMaxHp);

        moveCooldown = new CooldownController(this, jumpDelay.TimeSpan);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HpableExtension.IsFromWrongTeam(this, collision, out Bullet bullet))
        {
            this.Hp.TakeHp(bullet.Dmg, "Bullet");
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
    public void SetPistolVisibile(bool val)
    {
        gunObj.gameObject.SetActive(val);
    }
    private void Update()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        gunObj.transform.LookAt2D(mousePos);

        gunObj.Renderer.flipY = ((Vector2)this.transform.position).x > mousePos.x;
        curShotPoint = gunObj.Renderer.flipY ? leftShotPoint : rightShotPoint;
        gunObj.transform.position = curShotPoint.position;

        this.Renderer.flipX = !gunObj.Renderer.flipY;
        this.gunObj.transform.position = (!Renderer.flipX) ? leftPistolPos.position : rightPistolPos.position;

        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(Spawner.Instance.StartWave());
        }
    }
    public void TryMove(Direction dir)
    {
        if (moveCooldown.Try())
        {
            this.Rigidbody2D.MovePosition(this.transform.Get2DPos() + dir.ToVector2() * MoveSize);
        }
    }
    public Vector2 GetFrom()
    {
        return curShotPoint.position;
    }
    public Direction GetDirection()
    {
        return (Vector2)cam.ScreenToWorldPoint(Input.mousePosition) - this.transform.Get2DPos();
    }
    private void OnMovement(InputValue value)
    {
        TryMove(value.Get<Vector2>());
    }

}
