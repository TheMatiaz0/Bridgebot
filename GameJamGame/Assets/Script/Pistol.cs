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

    private readonly TimeSpan delay = TimeSpan.FromSeconds(0.1f);
    private readonly CooldownController cooldownController;

    public Pistol()
    {
        cooldownController = new CooldownController(Equipment.Instance, delay);
    }



    public override void OnUse()
    {
        base.OnUse();
        if (cooldownController.Try())
            BulletManager.Instance.Shoot(10, 1, Player.Instance.GetLookDirection(), Player.Instance.GetFrom(), Team.Good);
    }
    public override void OnStartSelect()
    {
        base.OnStartSelect();
        Player.Instance.SetPistolVisible(true);

    }
    public override void OnEndSelect()
    {
        base.OnEndSelect();
        Player.Instance.SetPistolVisible(false);
    }
}