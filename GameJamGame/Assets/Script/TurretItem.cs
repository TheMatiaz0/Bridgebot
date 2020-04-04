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
        // cooldownController = new CooldownController(Equipment.Instance, delay);
    }

    public TimeSpan Delay { get; }

    public override void OnUse()
    {
        base.OnUse();
        if (PlacementController.Instance.CanBuild)
        {
            PlacementController.Instance.OnPlace();
            Equipment.Instance.RemoveItem(this);
        }
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
