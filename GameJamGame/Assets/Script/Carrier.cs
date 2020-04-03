﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Cyberevolver.Unity;
using System;

public class Carrier : MonoBehaviourPlus, IHpable
{


	

	public Team CurrentTeam { get; private set; } = Team.Good;

	public Hp Hp { get; private set; }
    [Auto]
   public AIPath AIPath { get; private set; }


	[SerializeField]
	private Transform resourcesPoint = null;
    [SerializeField,RequiresAny]
    private Resource first;
    public Resource Current { get; private set; }

	[SerializeField]
	private uint startMaxHp;
    public bool IsLaunched { get; private set; }
    private Coroutine logic = null;
    private Bridge bridge;

    [SerializeField]
    private float speed = 10;

    private Transform currentTarget = null;

	protected void Start()
	{
		Hp = new Hp(startMaxHp, 0, startMaxHp);
		Hp.OnValueChangeToMin += Hp_OnValueChangeToMin;
	}

    protected override void Awake()
    {
        base.Awake();
        Current = first;
    }

    [Button]
    public bool Launch()
    {
        if (IsLaunched)
            return false;
        if (BridgeSelection.SelectedBridge is null || BridgeSelection.SelectedBridge.IsFixed)
            return false;
        IsLaunched = true;
        bridge = BridgeSelection.SelectedBridge;
        logic = StartCoroutine(Run());

        return true;
    }

    protected void Update()
    {
        if (currentTarget == null)
        {
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        //rb2D.MovePosition((Vector2)transform.position + (Vector2)currentTarget.position * speed * Time.fixedDeltaTime);
    }

    private IEnumerator Run()
    {
        while(true)
        {
            AIPath.canMove = true;
            AIPath.destination = Current.transform.position;
            yield return Async.NextFrame;
            yield return Async.Until(() => AIPath.reachedEndOfPath);
            AIPath.canMove = false;
            AIPath.canSearch = false;
            foreach (Transform item in Current.Points)
            {
                currentTarget = item;
                Debug.Log("Coming to point...");
                yield return Async.Until(() => Vector2.Distance(this.transform.position, item.position) <= 2);
            }
            AIPath.canMove = true;
            AIPath.canSearch = true;
            AIPath.destination = bridge.transform.position;
            Debug.Log("Coming to bridge...");
            yield return Async.NextFrame;
            yield return Async.Until(() => AIPath.reachedEndOfPath);

            if (bridge.RepairElement())
            {
                IsLaunched = false;
                Current = bridge.Next;
                bridge = null;
                break;
            }
        }
        

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {

        Bridge bridge = null; 
        if (bridge = collision.GetComponent<Bridge>())
        {
            Debug.Log($"{bridge.name} is here.");
        }
    }

    private void Hp_OnValueChangeToMin(object sender, Hp.HpChangedArgs e)
	{
		// game over!
		Debug.Log("R.I.P");
	}


}
