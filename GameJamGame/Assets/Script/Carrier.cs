using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Cyberevolver.Unity;
using System;

public class Carrier : MonoBehaviour, IHpable
{
	public enum Behaviours { IDLE, GETTING_RESOURCES, WALKING, BUILDING_BRIDGE}

	public Behaviours CurrentBehaviour { get; private set; } = Behaviours.IDLE;

	public Team CurrentTeam { get; private set; } = Team.Good;

	public Hp Hp { get; private set; }

	[SerializeField]
	private AIBase aiBase = null;

	[SerializeField]
	private AIPath path;

	[SerializeField]
	private Transform resourcesPoint = null;

	[SerializeField]
	private uint startMaxHp;


	protected void Start()
	{
		Hp = new Hp(startMaxHp, 0, startMaxHp);
		Hp.OnValueChangeToMin += Hp_OnValueChangeToMin;
	}

	private IEnumerator PickupResources ()
	{
		yield return null;
	}

	private void Hp_OnValueChangeToMin(object sender, Hp.HpChangedArgs e)
	{
		// game over!
		Debug.Log("R.I.P");
	}

	public IEnumerator LaunchCarrier ()
	{
		aiBase.destination = resourcesPoint.position;
		CurrentBehaviour = Behaviours.WALKING;

		yield return new WaitUntil(() => path.reachedEndOfPath == true);
		aiBase.destination = Vector2.zero;

		// animacja zbierania zasobów...
		yield return Async.Wait(TimeSpan.FromSeconds(3));

		if (BridgeSelection.SelectedBridge == null)
		{
			Debug.Log("WYBIERZ MOST");
			yield break;
		}
		
		aiBase.destination = BridgeSelection.SelectedBridge.transform.position;
		yield return Async.Wait(TimeSpan.FromSeconds(2));
		yield return new WaitUntil(() => path.reachedEndOfPath == true);
		aiBase.destination = Vector2.zero;
		// naprawa mostu...
		yield return Async.Wait(TimeSpan.FromSeconds(3));

		aiBase.destination = resourcesPoint.position;
	}
}
