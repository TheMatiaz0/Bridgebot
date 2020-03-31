using Cyberevolver;
using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	private Cint startHp = 10;

	public Cint CurrentHp { get; private set; }

	[SerializeField]
	private Rigidbody2D rb2D = null;

	[SerializeField]
	private float speed = 10;

	[SerializeField]
	private Cint takeDmg = 10;

	public void GetDamage(Cint dmgValue)
	{
		CurrentHp -= dmgValue;

		if (CurrentHp <= 0)
		{
			Destroy(this.gameObject);
		}
	}

	protected void Start()
	{
		CurrentHp = startHp;
	}

	protected void FixedUpdate()
	{
		// rb2D.MovePosition((Vector2)transform.position + targetDirection * speed * Time.deltaTime);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
	}
}
