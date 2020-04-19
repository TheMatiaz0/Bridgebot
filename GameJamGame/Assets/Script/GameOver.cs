using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cyberevolver.Unity;

public class GameOver : MonoBehaviour
{
	[SerializeField]
	private Text statText;

	[SerializeField]
	private FreezeMenu gameOverManager = null;

	[SerializeField]
	private AudioSource audioSource = null;

	[SerializeField]
	private AudioClip gameOverSound = null;

	[SerializeField]
	private Text mainText = null;

	private void Start()
	{
		statText.text = Statistics.Instance.GetStats();
		mainText.text = (Player.Instance.TheEndEnd) ? "This is the end of the demo, thank you for playing. :D" : "Your adventure has come to an end.\nGame over.";
		PhaseController.Instance.StopAllMusic();
		audioSource.PlayOneShot(gameOverSound);
	}

	public void OnRetry()
	{
		gameOverManager.EnableMenuWithPause(false);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void OnMenu()
	{
		gameOverManager.EnableMenuWithPause(false);
		SceneManager.LoadScene("MainMenu");
	}
}
