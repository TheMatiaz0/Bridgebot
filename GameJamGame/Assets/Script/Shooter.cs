using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
	[SerializeField]
	private SerializedTimeSpan timeForShoot;

	private CooldownController cooldownShoot = null;

	[SerializeField]
	private float bulletSpeed = 5;

	protected override void Update()
	{
		base.Update();

		if (base.CanSeePlayer())
		{
			if (cooldownShoot.Try())
			{
				Shoot();
			}
		}
	}

	private void Shoot ()
	{
		BulletManager.Instance.Shoot(bulletSpeed, Dmg, ((Vector2)(Player.Instance.transform.position - this.transform.position)).ToDirection(), this.transform.position, CurrentTeam);
	}
	
	protected override void Start()
	{
		base.Start();

		cooldownShoot = new CooldownController(this, timeForShoot.TimeSpan);
	}
}
