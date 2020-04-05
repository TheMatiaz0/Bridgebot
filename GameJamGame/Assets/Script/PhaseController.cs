using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseController : AutoInstanceBehaviour<PhaseController>
{
	public enum Phase { EXPLORING, PREPARATION, FIGHTING }

	public event EventHandler<Phase> OnPhaseChanged = delegate { };

	public Phase CurrentPhase { get { return _CurrentPhase; } private set { if (_CurrentPhase != value) { _CurrentPhase = value; } OnPhaseChanged.Invoke(this, _CurrentPhase); } }
	private Phase _CurrentPhase;

	[SerializeField]
	private WaveTimer timer = null;

	[SerializeField]
	private GameObject battleUI = null;

	[SerializeField]
	private SerializedTimeSpan timeToEndPreparation;

	private Coroutine spawnEnemies;

	public TimeSpan CurrentTimer { get { return _CurrentTimer; } private set { if (_CurrentTimer != value) { _CurrentTimer = value; } UpdateText(_CurrentTimer); } }
	private TimeSpan _CurrentTimer;

	private bool enableUpdate;

	protected void OnEnable()
	{
		OnPhaseChanged += PhaseController_OnPhaseChanged;
		BridgeSelection.OnBridgeSelected += OnBridgeSelected;
		Bridge.OnBridgeBuilt += Bridge_OnBridgeBuilt;
	}

	private void Bridge_OnBridgeBuilt(object sender, Cyberevolver.SimpleArgs<Bridge> e)
	{
		CurrentPhase = Phase.EXPLORING;
	}

	private void PhaseController_OnPhaseChanged(object sender, Phase e)
	{
		switch (e)
		{
			case Phase.FIGHTING:
				InfoManager.Instance.InfoObject.SetActive(true);
				InfoManager.Instance.InfoText.text = "Fighting phase. Monsters are coming!";
				enableUpdate = false;
				battleUI.SetActive(true);
				timer.gameObject.SetActive(false);
				if (spawnEnemies == null)
				{
					spawnEnemies = StartCoroutine(Spawner.Instance.StartWave());
				}
				break;

			case Phase.EXPLORING:
				InfoManager.Instance.InfoObject.SetActive(true);
				InfoManager.Instance.InfoText.text = "Exploring phase. Easy... you can relax now.";
				spawnEnemies = null;
				Spawner.Instance.KillAllEnemies();
				enableUpdate = false;
				battleUI.SetActive(false);
				timer.gameObject.SetActive(false);
				StopAllCoroutines();
				break;

			case Phase.PREPARATION:
				InfoManager.Instance.InfoObject.SetActive(true);
				InfoManager.Instance.InfoText.text = "Preparation phase. Better buckle your pants and hurry!";
				spawnEnemies = null;
				CurrentTimer = timeToEndPreparation.TimeSpan;
				enableUpdate = true;
				battleUI.SetActive(false);
				timer.gameObject.SetActive(true);
				StopAllCoroutines();
				break;
		}
	}


	protected void OnDisable()
	{
		OnPhaseChanged -= PhaseController_OnPhaseChanged;
		BridgeSelection.OnBridgeSelected -= OnBridgeSelected;
		Bridge.OnBridgeBuilt -= Bridge_OnBridgeBuilt;
	}

	private void UpdateText(TimeSpan currentTimer)
	{
		if (currentTimer <= TimeSpan.FromSeconds(5))
		{
			timer.TimerText.text = GenerateTimeInfo(currentTimer, true);
			return;
		}

		timer.TimerText.text = GenerateTimeInfo(currentTimer);
	}

	private string GenerateTimeInfo(TimeSpan currentTimer, bool ending = false)
	{
		if (ending == true)
		{
			return $"<color=red>{currentTimer.Minutes}:{currentTimer.Seconds:00}</color>";
		}

		else
		{
			return $"{currentTimer.Minutes}:{currentTimer.Seconds:00}";
		}
	}

	private void OnBridgeSelected(object sender, Cyberevolver.SimpleArgs<GameObject> e)
	{
		CurrentPhase = Phase.PREPARATION;
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
		if (CurrentTimer <= TimeSpan.Zero)
		{
			CurrentPhase = Phase.FIGHTING;
			return;
		}

		CurrentTimer = CurrentTimerPrepare();
	}

	private TimeSpan CurrentTimerPrepare()
	{
		return TimeSpan.FromSeconds(CurrentTimer.TotalSeconds - Time.deltaTime);
	}
}
