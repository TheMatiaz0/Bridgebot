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

	protected override void OnTriggerEnter2D(Collider2D collider)
	{
		base.OnTriggerEnter2D(collider);

		Player player = null;
		Carrier carrier = null;

		if (player = collider.GetComponent<Player>())
		{
			TakeDamage(player);
		}

		if (carrier = collider.GetComponent<Carrier>())
		{
			TakeDamage(carrier);
		}
	}

	private IEnumerator TakeDamage (IHpable entity)
	{
		entity.Hp.TakeHp(Dmg, "Rusher");
		yield return Async.Wait(attackCooldown.TimeSpan);
	}
}
