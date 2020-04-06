using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
	[SerializeField]
	private AudioMixer mixer = null;
	[SerializeField]
	private Dropdown qualityDropdown = null;
	[SerializeField]
	private Toggle vSyncToggle = null;
	[SerializeField]
	private Toggle fullscreenToggle = null;

	[SerializeField]
	private GameObject mainMainMenu = null;

	protected void Start()
	{
		qualityDropdown.SetValueWithoutNotify(QualitySettings.GetQualityLevel());
		vSyncToggle.SetIsOnWithoutNotify(Convert.ToBoolean(QualitySettings.vSyncCount));
		fullscreenToggle.SetIsOnWithoutNotify(Screen.fullScreen);
	}

	public void SetVolume(AudioMixer mixer, float value, string prefsName)
	{
		mixer.SetFloat(prefsName, Mathf.Log(value) * 20);
	}

	public void SetMusicVolume (bool isOn)
	{
		SetVolume(mixer, (isOn) ? 1f : 0.001f, "MusicVol");
	}

	public void SetSFXVolume (bool isOn)
	{
		SetVolume(mixer, (isOn) ? 1f : 0.001f, "SFXVol");
	}

	public void BackBtn ()
	{
		mainMainMenu.SetActive(true);
		this.gameObject.SetActive(false);
	}


	public void SetVSync(bool isOn)
	{
		QualitySettings.vSyncCount = Convert.ToInt32(isOn);
	}

	public void SetQuality(int qualityIndex)
	{
		QualitySettings.SetQualityLevel((int)qualityIndex);
	}

	public void SetFullscreen(bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
	}
}
