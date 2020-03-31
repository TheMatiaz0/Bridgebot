using Cyberevolver.Unity;
using Cyberevolver;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : AutoInstanceBehaviour<Player>
{
    [SerializeField]
    private SerializedTimeSpan jumpDelay = TimeSpan.FromSeconds(1);
    [SerializeField,ResetButton(1)]
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

    InputActions inputActions;
    private Transform curShotPoint;
    [SerializeField,Button("↺",Method =nameof(SetDefCamera))]
    private Camera cam;
    private void SetDefCamera()
    {
        cam = Camera.main;
    }
   
  
    protected override void Awake()
    {
        base.Awake();
        inputActions = new InputActions();

        moveCooldown = new CooldownController(this, jumpDelay.TimeSpan);
    }

    protected void OnEnable()
    {
        inputActions.Enable();
    }

    protected void OnDisable()
    {
        inputActions.Disable();
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
    }
    public void TryMove(Direction dir)
    {
        if(moveCooldown.Try())
        {
            this.Rigidbody2D.MovePosition(this.transform.Get2DPos() + dir.ToVector2()*MoveSize);
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
