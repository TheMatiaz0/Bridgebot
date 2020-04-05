using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver;
using Cyberevolver.Unity;
using System;

public class Rusher : Enemy
{
	[SerializeField]
	private SerializedTimeSpan attackCooldown;

	private CooldownController cooldown = null;

	protected new void Start()
	{
		base.Start();
		cooldown = new CooldownController(this, attackCooldown.TimeSpan);
	}

	protected override void OnTriggerEnter2D(Collider2D collider)
	{
		base.OnTriggerEnter2D(collider);

		Player player = null;
		Carrier carrier = null;

		if (player = collider.GetComponent<Player>())
		{
			Bite(player);
		}

		if (carrier = collider.GetComponent<Carrier>())
		{
			Bite(carrier);
		}
	}

	private void Bite (IHpable entity)
	{
		if (cooldown.Try())
		{
			entity.Hp.TakeHp(Dmg, "Rusher");
		}
	}
}
