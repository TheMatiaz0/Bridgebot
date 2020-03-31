using Cyberevolver.Unity;
using Cyberevolver;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviourPlus
{
    [SerializeField]
    private SerializedTimeSpan jumpDelay = TimeSpan.FromSeconds(1);
    [SerializeField,ResetButton(1)]
    private float MoveSize = 1;
    private Cyberevolver.Unity.CooldownController moveCooldown;

    InputActions inputActions;

    [Auto]
    public Rigidbody2D Rigidbody2D { get; private set; }
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

    public void TryMove(Direction dir)
    {
        if(moveCooldown.Try())
        {
            this.Rigidbody2D.MovePosition(this.transform.Get2DPos() + dir.ToVector2());
        }
    }

    private void OnMovement(InputValue value)
    {
        TryMove(value.Get<Vector2>());
    }

}
