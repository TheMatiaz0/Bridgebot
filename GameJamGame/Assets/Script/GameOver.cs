using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	public Text statText;

	private void Start()
	{
		GetStats();
	}

	public void GetStats()
	{
		statText.text = $"Bridges built: { Statistics.Instance.AllBridgeBuilt}\nEnemies defeated: {Statistics.Instance.AllKilledEnemies.Count}";
	}

	public void OnRetry()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void OnMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
