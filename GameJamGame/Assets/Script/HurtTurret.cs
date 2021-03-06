﻿using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver;
using System;
using System.Linq;

public class HurtTurret : Building
{
	[SerializeField]
	private float range = 40f;

	[SerializeField]
	private SerializedTimeSpan shootTimerMax;

	[SerializeField]
	private Transform shootPoint = null;

	[SerializeField]
	private float bulletSpeed = 40;

	[SerializeField]
	private Cint bulletDamage = 2;

	public override void OnPlace()
	{
		base.OnPlace();
		StartCoroutine(ShootWithDelay());
	}

	private IEnumerator ShootWithDelay()
	{
		while (true)
		{
			Enemy enemy = Spawner.Instance.GetClosestEnemy(shootPoint.transform.position, range);
			if (enemy != null)
			{
				this.transform.LookAtNorth2D(enemy.transform.position);
				BulletManager.Instance.Shoot(bulletSpeed, bulletDamage,  enemy.transform.Get2DPos()-this.transform.Get2DPos(), shootPoint.position, CurrentTeam);
			}

			yield return Async.Wait(shootTimerMax.TimeSpan);
		}
	}
}
