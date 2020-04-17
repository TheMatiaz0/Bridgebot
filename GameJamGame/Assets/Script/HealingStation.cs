using Cyberevolver;
using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingStation : Building
{
	[SerializeField]
	private SerializedTimeSpan cooldown;

	[SerializeField]
	private Cint hpAdd = 1;

	private CooldownController cooldownController = null;


	protected void Start()
	{
		cooldownController = new CooldownController(this, cooldown.TimeSpan);
	}

	protected override void OnTriggerStay2D(Collider2D collision)
	{
		base.OnTriggerStay2D(collision);	
		Carrier carrier = null;

		
		if (IsPlaced && cooldownController.Try())
		{
			if (carrier = collision.GetComponent<Carrier>())
			{
				Heal(carrier);
			}

		}

	}

	private void Heal (IHpable hpable)
	{
		if (IsPlaced == true)
		{
			hpable.Hp.GiveHp(hpAdd, "Healing Station");
		}	
	}
}
