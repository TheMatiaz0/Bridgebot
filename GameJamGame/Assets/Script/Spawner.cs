using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cyberevolver;

public class Spawner : AutoInstanceBehaviour<Spawner>
{
	public List<Enemy> SpawnedEnemies { get; private set; } = new List<Enemy>();

	public enum SpawnState { SPAWNING, WAITING_FOR_ENEMY_DEATH, SELECTING_BRIDGE };

	[SerializeField]
	private Transform[] enemySpawners;

	[SerializeField]
	private Enemy enemyType;

	[SerializeField]
	private Cint enemyCount = 1;

	public SpawnState CurrentState { get; private set; } = SpawnState.SPAWNING;

	[SerializeField]
	private SerializedTimeSpan timeInterval;

	[SerializeField]
	private SerializedTimeSpan timeBetweenWaves;

	private bool AnyEnemyisAlive()
	{
		SpawnedEnemies = SpawnedEnemies.Where(e => e != null).ToList();

		return SpawnedEnemies.Count > 0;
	}

	private IEnumerator StartWave()
	{
		while (CurrentState != SpawnState.SELECTING_BRIDGE)
		{
			CurrentState = SpawnState.SPAWNING;

			yield return SpawnWave();

			CurrentState = SpawnState.WAITING_FOR_ENEMY_DEATH;

			yield return new WaitWhile(AnyEnemyisAlive);

			// yield return Async.Wait(timeBetweenWaves.TimeSpan);
		}
	}

	private IEnumerator SpawnWave()
	{
		for (int i = 0; i < enemyCount; i++)
		{
			SpawnEnemy();
			yield return Async.Wait(timeInterval.TimeSpan);
		}
	}

	public Enemy GetClosestEnemy(Vector2 currentPosition, float range)
	{
		Enemy bestTarget = null;
		foreach (Enemy enemy in SpawnedEnemies)
		{
			if (enemy.gameObject == null)
			{
				continue;
			}

			Vector2 directionToTarget = (Vector2)enemy.transform.position - currentPosition;
			float dSqrToTarget = directionToTarget.sqrMagnitude;
			if (dSqrToTarget < range)
			{
				range = dSqrToTarget;
				bestTarget = enemy;
			}
		}

		return bestTarget;
	}

	private void SpawnEnemy()
	{
		Enemy tempEnemy;
		int t = UnityEngine.Random.Range(0, enemySpawners.Length);
		SpawnedEnemies.Add(tempEnemy = Instantiate(enemyType, enemySpawners[t].position, Quaternion.identity).GetComponent<Enemy>());
	}
}
