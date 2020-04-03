using Cyberevolver;
using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Enemy : MonoBehaviour, IHpable
{
	[SerializeField]
	private Cint startHp = 10;

	[SerializeField]
	private Rigidbody2D rb2D = null;

	private Transform targetTransform = null;

	[SerializeField]
	private AIBase aiBase;

	public Team CurrentTeam { get; private set; } = Team.Bad;

	public Hp Hp { get; private set; }

	[SerializeField]
	private Cint takeDmg;

	public Cint TakeDmg => takeDmg;


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
		if (Player.Instance != null)
		{
			targetTransform = Player.Instance.transform;
			StartCoroutine(CheckPlayerPosition());
		}
	}

	private IEnumerator CheckPlayerPosition ()
	{
		while (true)
		{
			aiBase.destination = targetTransform.position;

			yield return Async.Wait(TimeSpan.FromMilliseconds(600));
		}

		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (HpableExtension.IsFromWrongTeam(this, collision, out Bullet bullet))
		{
			this.Hp.TakeHp(bullet.Dmg, "Bullet");
			Destroy(bullet.gameObject);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.GetComponent<Player>())
		{

		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
	}
}
