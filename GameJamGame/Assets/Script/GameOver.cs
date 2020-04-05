using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	[SerializeField]
	private Text statText;

	private void Start()
	{
		statText.text = Statistics.Instance.GetStats();
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
