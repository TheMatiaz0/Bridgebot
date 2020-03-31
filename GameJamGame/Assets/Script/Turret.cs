using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver;

public class Turret : MonoBehaviour
{
	[SerializeField]
	private float range = 40f;

	[SerializeField]
	private SerializedTimeSpan shootTimerMax;

	[SerializeField]
	private Transform shootPoint = null;

	[SerializeField]
	private GameObject bulletPrefab = null;

	[SerializeField]
	private float bulletSpeed = 40;

	[SerializeField]
	private Cint bulletDamage = 2;

	protected void Start()
	{
		StartCoroutine(ShootWithDelay());
	}

	private IEnumerator ShootWithDelay()
	{
		while (true)
		{
			Enemy enemy = Spawner.Instance.GetClosestEnemy(shootPoint.transform.position, range);
			if (enemy != null)
			{
				BulletManager.Instance.Shoot(bulletSpeed, bulletDamage, (Vector2)enemy.transform.position, shootPoint.position, Team.Good);
			}

			yield return Async.Wait(shootTimerMax.TimeSpan);
		}
	}
}
