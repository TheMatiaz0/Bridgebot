using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IHpable
{

	public Team CurrentTeam { get; private set; } = Team.Good;

	public Hp Hp { get; private set; }

	public bool IsPlaced { get; private set; }

	public virtual void OnPlace ()
	{
		IsPlaced = true;
	}

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if (IsPlaced == false)
		{
			return;
		}

		if (HpableExtension.IsFromWrongTeam(this, collision, out Bullet bullet))
		{
			this.Hp.TakeHp(bullet.Dmg, "Bullet");
			bullet.Kill();
		}
	}
}
