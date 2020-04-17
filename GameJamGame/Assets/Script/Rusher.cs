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
    

  
    [SerializeField]
    private AudioSource audioSource = null;

    [SerializeField]
    private AudioClip bite = null;

    private IHpable bitter = null;
	protected new void Start()
	{
		base.Start();
		cooldown = new CooldownController(this, attackCooldown.TimeSpan);
      

	}
    protected override void Update()
    {
        base.Update();
       
        if (((UnityEngine.Object)bitter) != null)
            TryBite(bitter);

    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        IHpable entity;
        if (Check(collision, out entity))
            bitter = entity;
    }
    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        if(Check(collision,out IHpable entity))
        {
            if (entity == bitter)
                bitter = null;
        }
    }
    private bool Check(Collider2D col, out IHpable entity)
    {
        IHpable moment;
        if ((moment = col.GetComponent<IHpable>()) != null && moment is Enemy == false)
        {
            entity = moment;
            return true;
        }
        entity = null;
        return false;
          

    }
   



    private void TryBite (IHpable entity)
	{
		if (cooldown.Try())
		{
			entity.Hp.TakeHp(Dmg, "Rusher");
            audioSource.PlayOneShot(bite);
		}
	}
}
