using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
public class Pistol : Item
{
    public Pistol(TimeSpan delay)
    {
        Delay = delay;
        cooldownController = new CooldownController(Equipment.Instance,delay);
       
    }

    public TimeSpan Delay { get; }
    private readonly CooldownController cooldownController;
    public override void OnUse()
    {
        base.OnUse();
        if(Input.GetMouseButtonDown(0)&&cooldownController.Try())
        {
            BulletManager.Instance.Shoot(10, 1, Direction.Up, Player.Instance.transform.Get2DPos(), Team.Good);
        }
    }
}