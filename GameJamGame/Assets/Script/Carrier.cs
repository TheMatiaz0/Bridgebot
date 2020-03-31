using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Cyberevolver.Unity;
using System;

public class Carrier : MonoBehaviour
{
	public enum Behaviours { IDLE, GETTING_RESOURCES, WALKING, BUILDING_BRIDGE}

	public Behaviours CurrentBehaviour { get; private set; } = Behaviours.IDLE;

	[SerializeField]
	private AIBase aiBase = null;

	[SerializeField]
	private AIPath path;

	[SerializeField]
	private Transform resourcesPoint = null;

	protected void Start()
	{
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
