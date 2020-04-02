using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseController : AutoInstanceBehaviour<PhaseController>
{
	public enum Phase { EXPLORING, PREPARATION, FIGHTING }

	public event EventHandler<Phase> OnPhaseChanged = delegate { };

	public Phase CurrentPhase { get { return _CurrentPhase; } private set { if (_CurrentPhase != value) { _CurrentPhase = value; } OnPhaseChanged.Invoke(this, _CurrentPhase); } }
	private Phase _CurrentPhase;

	[SerializeField]
	private WaveTimer timer = null;

	[SerializeField]
	private SerializedTimeSpan timeToEndPreparation;

	private Coroutine spawnEnemies;

	public TimeSpan CurrentTimer { get { return _CurrentTimer; } private set { if (_CurrentTimer != value) { _CurrentTimer = value; } UpdateText(_CurrentTimer); } }
	private TimeSpan _CurrentTimer;

	private bool enableUpdate;

	protected void Start()
	{
		CurrentTimer = timeToEndPreparation.TimeSpan;
	}

	protected void OnEnable()
	{
		BridgeSelection.OnBridgeSelected += OnBridgeSelected;
		OnPhaseChanged += PhaseController_OnPhaseChanged;
	}

	private void PhaseController_OnPhaseChanged(object sender, Phase e)
	{
		switch (e)
		{
			case Phase.FIGHTING:
				if (spawnEnemies == null)
				{
					spawnEnemies = StartCoroutine(Spawner.Instance.StartWave());
				}
				break;
		}
	}


	protected void OnDisable()
	{
		OnPhaseChanged -= PhaseController_OnPhaseChanged;
		BridgeSelection.OnBridgeSelected -= OnBridgeSelected;
	}

	private void UpdateText(TimeSpan currentTimer)
	{
		timer.TimerText.text = $"{currentTimer.Minutes}:{currentTimer.Seconds:00}";
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

	private void CalculateTimer()
	{
		if (CurrentTimer <= TimeSpan.Zero == true)
		{
			CurrentPhase = Phase.FIGHTING;
			return;
		}

		CurrentTimer = CurrentTimerPrepare();
	}

	private TimeSpan CurrentTimerPrepare ()
	{
		return TimeSpan.FromSeconds(CurrentTimer.TotalSeconds - Time.deltaTime);
	}
}
