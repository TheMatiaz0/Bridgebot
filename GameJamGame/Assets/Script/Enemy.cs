﻿using Cyberevolver;
using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour, IHpable
{
	[SerializeField]
	private Cint startHp = 10;

	[SerializeField]
	private Rigidbody2D rb2D = null;

	[SerializeField]
	private float speed = 10;

	[SerializeField]
	private Cint takeDmg = 10;

	[SerializeField]
	private AIDestinationSetter setter;

	public Team CurrentTeam { get; private set; } = Team.Bad;

	public Hp Hp { get; private set; }


	protected void Awake()
	{
		Hp = new Hp(startHp, 0, startHp);
		Hp.OnValueChangeToMin += Hp_OnValueChangeToMin;
	}

	private void Hp_OnValueChangeToMin(object sender, Hp.HpChangedArgs e)
	{
		Destroy(this.gameObject);
	}

	protected void Start()
	{
		setter.target = Player.Instance.transform;
	}

	protected void FixedUpdate()
	{
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (HpableExtension.IsFromWrongTeam(this, collision, out Bullet bullet))
		{
			this.Hp.TakeHp(bullet.Dmg, "Bullet");
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
	}
}
