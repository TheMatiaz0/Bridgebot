using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class Pistol : Item
{
    public Pistol(TimeSpan delay)
    {
        Delay = delay;
        cooldownController = new CooldownController(Equipment.Instance, delay);

    }

    public TimeSpan Delay { get; }
    private readonly CooldownController cooldownController;
    public override void OnUse()
    {
        base.OnUse();
        if (Input.GetMouseButtonDown(0) && cooldownController.Try())
        {
            BulletManager.Instance.Shoot(10, 1, Player.Instance.GetDirection(), Player.Instance.GetFrom(), Team.Good);
        }
    }
    public override void OnStartSelect()
    {
        base.OnStartSelect();
        Player.Instance.SetPistolVisibile(true);

    }
    public override void OnEndSelect()
    {
        base.OnEndSelect();
        Player.Instance.SetPistolVisibile(false);
    }
}