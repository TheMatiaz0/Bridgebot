using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseController : AutoInstanceBehaviour<PhaseController>
{
	public enum Phase { EXPLORING, PREPARATION, FIGHTING }

	public Phase CurrentPhase { get; private set; } = Phase.EXPLORING;

	[SerializeField]
	private WaveTimer timer = null;

	[SerializeField]
	private SerializedTimeSpan timeToEndPreparation;

	private TimeSpan startTime;

	public TimeSpan CurrentTimer { get { return _CurrentTimer; } private set {_CurrentTimer = value; UpdateText(_CurrentTimer); } }
	private TimeSpan _CurrentTimer;

	private bool enableUpdate;

	protected void Start()
	{
		startTime = TimeSpan.FromSeconds(Time.time);
		CurrentTimer = timeToEndPreparation.TimeSpan - (TimeSpan.FromSeconds(Time.time) - startTime);
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

	private void UpdateText (TimeSpan currentTimer)
	{
		timer.TimerText.text = $"{currentTimer.Minutes}:{currentTimer.Seconds:00}";
	}

	private void CalculateTimer()
	{
		if (CurrentTimer <= TimeSpan.Zero == true)
		{
			Debug.Log("End");
			return;
		}

		CurrentTimer = timeToEndPreparation.TimeSpan - (TimeSpan.FromSeconds(Time.time) - startTime);
	}
}
