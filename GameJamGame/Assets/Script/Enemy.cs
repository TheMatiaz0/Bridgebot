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
	private Cint dmg;

	public Cint Dmg => dmg;

	[SerializeField]
	private float minDistance = 5;


	protected virtual void Awake()
	{
		Hp = new Hp(startHp, 0, startHp);
		Hp.OnValueChangeToMin += Hp_OnValueChangeToMin;
	}

	private void Hp_OnValueChangeToMin(object sender, Hp.HpChangedArgs e)
	{
		Destroy(this.gameObject);
	}

	protected virtual void Start()
	{
		if (Player.Instance != null)
		{
			targetTransform = Player.Instance.transform;
			StartCoroutine(CheckPlayerPosition());
		}
	}

	protected virtual void Update()
	{
		
	}

	private IEnumerator CheckPlayerPosition()
	{
		while (true)
		{
			aiBase.destination = targetTransform.position;

			yield return Async.Wait(TimeSpan.FromMilliseconds(600));
		}
	}

	public bool CanSeePlayer ()
	{
		float dis = Vector2.Distance(Player.Instance.transform.position, this.transform.position);
		return dis <= minDistance;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (HpableExtension.IsFromWrongTeam(this, collision, out Bullet bullet))
		{
			this.Hp.TakeHp(bullet.Dmg, "Bullet");
			Destroy(bullet.gameObject);
		}
	}


	protected virtual void OnCollisionEnter2D(Collision2D collision)
	{
	}

	protected virtual void OnCollisionExit2D(Collision2D collision)
	{
	}
}
