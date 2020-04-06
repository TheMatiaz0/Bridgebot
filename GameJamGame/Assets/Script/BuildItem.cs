using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildItem : Item
{
    public override void OnUse()
    {
        base.OnUse();
        if (PhaseController.Instance.CurrentPhase == PhaseController.Phase.PREPARATION)
        {
            if (PlacementController.Instance.CanBuild)
            {
                PlacementController.Instance.OnPlace();
                Equipment.Instance.RemoveItem(this);

            }
        }

        else
        {
            InfoManager.Instance.InfoObject.SetActive(true);
            InfoManager.Instance.InfoText.text = "Building allowed only during Preparation.";
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
