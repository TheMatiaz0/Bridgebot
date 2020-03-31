using Cyberevolver.Unity;
using Cyberevolver;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AutoInstanceBehaviour<Player>
{
    [SerializeField]
    private SerializedTimeSpan jumpDelay = TimeSpan.FromSeconds(1);
    [SerializeField,ResetButton(1)]
    private float MoveSize = 1;
    private Cyberevolver.Unity.CooldownController moveCooldown;

    [Auto]
    public Rigidbody2D Rigidbody2D { get; private set; }
    [SerializeField]
    private ItemAsset pistol;
    [SerializeField]
    private ItemAsset turret;
    protected override void Awake()
    {
        base.Awake();
        moveCooldown = new CooldownController(this, jumpDelay.TimeSpan);
    }
    public void TryMove(Direction dir)
    {
        if(moveCooldown.Try())
        {
            this.Rigidbody2D.MovePosition(this.transform.Get2DPos() + dir.ToVector2()*MoveSize);
        }
    }
    protected virtual void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            TryMove(Direction.Up);
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            TryMove(Direction.Down);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            TryMove(Direction.Left);
        if(Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))
            TryMove(Direction.Right);
    }

}
