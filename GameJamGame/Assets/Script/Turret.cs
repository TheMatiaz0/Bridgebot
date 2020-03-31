using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver;

public class Turret : MonoBehaviour, IHpable
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

	public Team CurrentTeam { get; private set; } = Team.Good;

	public Hp Hp { get; private set; }

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
				BulletManager.Instance.Shoot(bulletSpeed, bulletDamage,  enemy.transform.Get2DPos()-this.transform.Get2DPos(), shootPoint.position, CurrentTeam);
			}

			yield return Async.Wait(shootTimerMax.TimeSpan);
		}
	}
}
