using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretItem : Item
{
    public TurretItem(TimeSpan delay)
    {
        Delay = delay;
        cooldownController = new CooldownController(Equipment.Instance, delay);
    }

    public TimeSpan Delay { get; }
    private readonly CooldownController cooldownController;

    public override void OnUse()
    {
        base.OnUse();
        PlacementController.Instance.OnPlace();

    }
    public override void OnStartSelect()
    {
        base.OnStartSelect();
        PlacementController.Instance.Activate(Prefab);

    }
    public override void OnEndSelect()
    {
        base.OnEndSelect();
        PlacementController.Instance.Deactive();
    }
}
