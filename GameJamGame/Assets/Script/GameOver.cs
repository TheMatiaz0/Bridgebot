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

	private void Start()
	{
		statText.text = Statistics.Instance.GetStats();
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
