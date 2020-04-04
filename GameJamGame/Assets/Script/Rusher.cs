using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rusher : Enemy
{
	protected override void OnCollisionEnter2D(Collision2D collision)
	{
		Player player = null;
		Carrier carrier = null;

		if ((player = collision.collider.GetComponent<Player>()))
		{
			player.Hp.TakeHp(Dmg, "Rusher");
		}

		if ((carrier = collision.collider.GetComponent<Carrier>()))
		{
			carrier.Hp.TakeHp(Dmg, "Rusher");
		}
	}
}
