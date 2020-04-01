using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseController : AutoInstanceBehaviour<PhaseController>
{
	public enum Phase { EXPLORING, PREPARATION, FIGHTING }

	public Phase CurrentPhase = Phase.EXPLORING;

	[SerializeField]
	private WaveTimer timer = null;

	[SerializeField]
	private SerializedTimeSpan timeToEndPreparation;

	private TimeSpan startTime;

	private TimeSpan currentTimer;

	private bool enableUpdate;

	protected new void Awake()
	{
		base.Awake();


		startTime = TimeSpan.FromSeconds(Time.time);
	}

	protected void OnEnable()
	{
		BridgeSelection.OnBridgeSelected += OnBridgeSelected;
	}

	protected void OnDisable()
	{
		BridgeSelection.OnBridgeSelected -= OnBridgeSelected;
	}

	private void OnBridgeSelected(object sender, Cyberevolver.SimpleArgs<GameObject> e)
	{
		CurrentPhase = Phase.PREPARATION;
		// currentTimer = timeToEndPreparation.TimeSpan - (TimeSpan.FromSeconds(Time.time) - startTime);
		enableUpdate = true;
	}

	protected void Update()
	{
		if (enableUpdate == false)
		{
			return;
		}

		CalculateTimer();
	}

	private void CalculateTimer()
	{
		if (currentTimer <= TimeSpan.Zero == true)
		{
			Debug.Log("End");
			return;
		}

		currentTimer = timeToEndPreparation.TimeSpan - (TimeSpan.FromSeconds(Time.time) - startTime);
		timer.TimerText.text = $"{currentTimer.Minutes}:{currentTimer.Seconds:00}";
	}
}
