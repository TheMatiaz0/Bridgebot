using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cyberevolver.Unity;
using Cyberevolver;

public class Bridge : MonoBehaviourPlus
{
	public bool IsFixed { get { return _IsFixed; } private set { if (_IsFixed != value) { _IsFixed = value; RemoveTriggerOnFixed(); } } }
    private bool _IsFixed;

	[SerializeField]
	private uint needResourcesCount = 1;

    [SerializeField]
    private Resource next;
    public Resource Next => next;

 
    [field:SerializeField]
    public Transform BulidPoint { get; private set; }

    [SerializeField]
    private BoxCollider2D triggerCollider = null;

    private void RemoveTriggerOnFixed ()
    {
        Destroy(triggerCollider);
    }

	public bool RepairElement ()
	{
        if (needResourcesCount == 0)
        {
            FullRepair();
            return true;
        }
           
        needResourcesCount--;
        return false;
	}
    public void FullRepair()
    {

    }
}
