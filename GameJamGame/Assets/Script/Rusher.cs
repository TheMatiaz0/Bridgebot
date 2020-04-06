using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver;
using Cyberevolver.Unity;
using System;
using Pathfinding;

public class Rusher : Enemy
{
	[SerializeField]
	private SerializedTimeSpan attackCooldown;

	private CooldownController cooldown = null;
    [Auto]
    public AIPath Path { get; private set; }
    [Auto]
    public Animator Animator { get; protected set; }

	protected new void Start()
	{
		base.Start();
		cooldown = new CooldownController(this, attackCooldown.TimeSpan);
		// cooldown = new CooldownController(this, attackCooldown.TimeSpan);
      

	}
    protected override void Update()
    {
        base.Update();
        Animator.SetBool("Move", !Path.reachedEndOfPath);

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
