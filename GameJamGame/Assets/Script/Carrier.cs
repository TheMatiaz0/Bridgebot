using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Carrier : MonoBehaviour
{
	public enum Behaviours { }

	[SerializeField]
	private AIBase aiBase = null;

	[SerializeField]
	private Transform startIsland = null;

	protected void Start()
	{
	}

	private IEnumerator LaunchCarrier ()
	{
		yield return null;
	}
}
