using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cyberevolver.Unity;
using Cyberevolver;

public class Bridge : MonoBehaviour
{
	public bool IsFixed { get; private set; } = false;

	[SerializeField]
	private uint needResourcesCount = 20;

	[SerializeField]
	private SerializedTimeSpan timeToFullRepair;

	protected void Start()
	{
	}


	public void Repair ()
	{
		// AsyncStoper.MakeSimple(this, timeToFullRepair.TimeSpan, );
	}
}
