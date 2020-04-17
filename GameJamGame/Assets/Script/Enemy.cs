using Cyberevolver;
using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Enemy : MonoBehaviourPlus, IHpable
{
	[SerializeField]
	private Cint startHp = 10;

	protected Transform carrierEntity = null;
	protected Transform playerEntity = null;

	[SerializeField]
	private AIBase aiBase;
    [SerializeField,AssetOnly]
    private GameObject deathEffect = null;

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

	[Auto]
	public Animator Animator { get; private set; }
	[Auto]
	public AIPath Path { get; private set; }


	protected override void Awake()
	{
        base.Awake();
		Hp = new Hp(startHp, 0, startHp);
		Hp.OnValueChangeToMin += Hp_OnValueChangeToMin;
	}

	private void Hp_OnValueChangeToMin(object sender, Hp.HpChangedArgs e)
	{
		Statistics.Instance.AllKilledEnemies += 1;
        if(deathEffect!=null)
        {
            Instantiate(deathEffect).transform.position = this.transform.position;
        }
		Destroy(this.gameObject);
	}


	protected virtual void Start()
	{
		carrierEntity = GameObject.FindGameObjectWithTag("Carrier").transform;

		StartCoroutine(CheckTargetPosition(carrierEntity));
	}

	protected virtual void Update()
	{
		Animator.SetBool("Move", !Path.reachedEndOfPath);
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
