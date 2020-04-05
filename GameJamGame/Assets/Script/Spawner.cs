using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cyberevolver;

public class Spawner : AutoInstanceBehaviour<Spawner>
{
	public List<Enemy> SpawnedEnemies { get; private set; } = new List<Enemy>();

	[SerializeField]
	private Transform[] enemySpawners;

	public Transform[] EnemySpawners { get; set; }

	[SerializeField]
	private Enemy[] enemyTypes;

	[SerializeField]
	private Cint enemyCount = 1;

	[SerializeField]
	private SerializedTimeSpan timeInterval;

	[SerializeField]
	private SerializedTimeSpan timeBetweenWaves;

	/*
	private bool AnyEnemyisAlive()
	{
		SpawnedEnemies = SpawnedEnemies.Where(e => e != null).ToList();

		return SpawnedEnemies.Count > 0;
	}
	*/

	protected new void Awake()
	{
		base.Awake();
		EnemySpawners = enemySpawners;
	}

	protected void Start()
	{
		PhaseController.Instance.OnPhaseChanged += OnPhaseChanged;
	}

	private void OnPhaseChanged(object sender, PhaseController.Phase e)
	{
		switch (e)
		{
			case PhaseController.Phase.EXPLORING:
			case PhaseController.Phase.PREPARATION:
				StopCoroutine(StartWave());
				KillAllEnemies();
				break;

		}
	}

	public IEnumerator StartWave()
	{
		while (true)
		{
			yield return SpawnWave();

			// yield return new WaitWhile(AnyEnemyisAlive);

			yield return Async.Wait(timeBetweenWaves.TimeSpan);
		}
	}

	public void KillAllEnemies()
	{
		foreach (Enemy item in SpawnedEnemies)
		{
			if (item == null)
			{
				continue;
			}

			Destroy(item.gameObject);
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
			if (enemy == null)
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
		int k = UnityEngine.Random.Range(0, enemyTypes.Length);
		int t = UnityEngine.Random.Range(0, EnemySpawners.Length);
		SpawnedEnemies.Add(tempEnemy = Instantiate(enemyTypes[k], EnemySpawners[t].position, Quaternion.identity).GetComponent<Enemy>());
		tempEnemy.gameObject.layer = 11;
	}
}
