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

	protected Transform carrierEntity = null;
	protected Transform playerEntity = null;

	[SerializeField]
	private AIBase aiBase;

	public AIBase AIBase => aiBase;

	public Team CurrentTeam { get; private set; } = Team.Bad;

	public Hp Hp { get; private set; }

	[SerializeField]
	private Cint dmg;

	public Cint Dmg => dmg;

	[SerializeField]
	private float minDistance = 5;

	[SerializeField]
	private SpriteRenderer spriteRender = null;


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
		carrierEntity = GameObject.FindGameObjectWithTag("Carrier").transform;

		StartCoroutine(CheckTargetPosition(carrierEntity));
	}

	protected virtual void Update()
	{
		if (aiBase.destination.x > 1)
		{
			spriteRender.flipX = true;
		}

		else if (aiBase.destination.x < -1)
		{
			spriteRender.flipX = false;
		}
	}

	protected IEnumerator CheckTargetPosition(Transform transform)
	{
		while (true)
		{
			aiBase.destination = transform.position;

			yield return Async.Wait(TimeSpan.FromMilliseconds(600));
		}
	}

	protected bool CanSeeTarget (Transform transform)
	{
		float dis = Vector2.Distance(transform.position, this.transform.position);
		return dis <= minDistance;
	}

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if (HpableExtension.IsFromWrongTeam(this, collision, out Bullet bullet))
		{
			this.Hp.TakeHp(bullet.Dmg, "Bullet");
			bullet.Kill();
		}
	}

	protected void OnDestroy()
	{
		Statistics.Instance.AllKilledEnemies.Add(this);
	}

	protected virtual void OnTriggerExit2D(Collider2D collision)
	{

	}


	protected virtual void OnCollisionEnter2D(Collision2D collision)
	{
	}

	protected virtual void OnCollisionExit2D(Collision2D collision)
	{
	}
}
