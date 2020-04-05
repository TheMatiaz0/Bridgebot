using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
	Statistics playerStats;
	public Text statText;

	private void Start()
	{
		playerStats = GetComponent<Statistics>();
		GetStats();
	}

	public void GetStats()
	{
		statText.text = $"Bridges built: { playerStats.AllBridgeBuilt}\n Enemies defeated: {playerStats.AllKilledEnemies}";
	}

	public void OnRetry()
	{

	}

	public void OnMenu()
	{

	}
	void Update()
	{
		Debug.Log(playerStats.AllKilledEnemies + "/" + playerStats.AllBridgeBuilt);
	}
}
