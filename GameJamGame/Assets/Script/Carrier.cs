using System.Collections;
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
        if (BridgeSelection.SelectedBridge==null||BridgeSelection.SelectedBridge)
            return false;
        IsLaunched = true;
        bridge = BridgeSelection.SelectedBridge;
        logic = StartCoroutine(Run());
        return true;
    }
    private IEnumerator Run()
    {
        while(true)
        {
            AIPath.canMove = true;
            AIPath.destination = Current.transform.position;
            yield return Async.NextFrame;
            yield return AIPath.reachedEndOfPath;
            AIPath.canMove = false;
            foreach (var item in Current.Points)
            {
                bool end = false;
                var tween = LeanTween.move(this.gameObject, item.position, Vector2.Distance(item.transform.position, item.position))
                    .setOnComplete(() => end = true);


                yield return Async.While(() => end == false);

            }
            AIPath.canMove = true;
            AIPath.destination = bridge.transform.position;
            yield return Async.NextFrame;
            yield return AIPath.reachedEndOfPath;
            if (bridge.RepairElement())
            {
                IsLaunched = false;
                Current = bridge.Next;
                bridge = null;
                break;
            }
        }
        

    }

	

	private void Hp_OnValueChangeToMin(object sender, Hp.HpChangedArgs e)
	{
		// game over!
		Debug.Log("R.I.P");
	}


}
