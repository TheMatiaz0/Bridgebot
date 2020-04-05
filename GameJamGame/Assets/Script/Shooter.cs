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

		if (base.CanSeeTarget(playerEntity))
		{
			AIBase.canMove = false;
			if (cooldownShoot.Try())
			{
				Shoot(playerEntity);
			}
		}

		else
		{
			AIBase.canMove = true;
		}
	}

	private void Shoot (Transform target)
	{
		BulletManager.Instance.Shoot(bulletSpeed, Dmg, ((Vector2)(target.position - this.transform.position)).ToDirection(), this.transform.position, CurrentTeam);
	}
	
	protected override void Start()
	{
		playerEntity = Player.Instance.transform;

		cooldownShoot = new CooldownController(this, timeForShoot.TimeSpan);

		StartCoroutine(CheckTargetPosition(playerEntity));
	}
}
