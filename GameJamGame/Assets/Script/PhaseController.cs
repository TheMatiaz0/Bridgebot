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
	private Coroutine musicProgress;

	[SerializeField]
	private AudioSource[] musicTracks = null;

	public TimeSpan CurrentTimer { get { return _CurrentTimer; } private set { if (_CurrentTimer != value) { _CurrentTimer = value; } UpdateText(_CurrentTimer); } }
	private TimeSpan _CurrentTimer;

	public bool EnableUpdate { get; set; }

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
			case Phase.EXPLORING:
				StopAllCoroutines();
				musicTracks[1].Stop();
				musicTracks[2].Stop();
				InfoManager.Instance.InfoObject.SetActive(true);
				InfoManager.Instance.InfoText.text = "Exploring phase. You can relax now.";
				spawnEnemies = null;
				Spawner.Instance.KillAllEnemies();
				EnableUpdate = false;
				battleUI.SetActive(false);
				timer.gameObject.SetActive(false);
				break;

			case Phase.PREPARATION:
				StopAllCoroutines();
				AddLayerMusic(1);
				// musicTracks[1].Play();
				InfoManager.Instance.InfoObject.SetActive(true);
				InfoManager.Instance.InfoText.text = "Preparation phase. Get ready!";
				spawnEnemies = null;
				CurrentTimer = timeToEndPreparation.TimeSpan;
				EnableUpdate = true;
				battleUI.SetActive(false);
				timer.gameObject.SetActive(true);
				break;

			case Phase.FIGHTING:
				AddLayerMusic(2);
				InfoManager.Instance.InfoObject.SetActive(true);
				InfoManager.Instance.InfoText.text = "Fighting phase. Monsters are coming!";
				EnableUpdate = false;
				battleUI.SetActive(true);
				timer.gameObject.SetActive(false);
				if (spawnEnemies == null)
				{
					spawnEnemies = StartCoroutine(Spawner.Instance.StartWave());
				}
				break;
		}
	}

	private void AddLayerMusic (int musicTrackNum)
	{
		bool restore0 = false;
		bool restore1 = false;
		if (musicTracks[0].isPlaying)
		{
			musicTracks[0].Stop();
			restore0 = true;

			if (musicTracks[1].isPlaying)
			{
				musicTracks[1].Stop();
				restore1 = true;
			}
		}

		musicTracks[musicTrackNum].Play();

		if (restore0)
		{
			musicTracks[0].Play();
		}

		if (restore1)
		{
			musicTracks[1].Play();
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
		if (EnableUpdate == false)
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
