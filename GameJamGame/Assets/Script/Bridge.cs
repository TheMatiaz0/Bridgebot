using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cyberevolver.Unity;
using Cyberevolver;

public class Bridge : MonoBehaviourPlus
{
	public bool IsFixed { get; private set; } = false;

	[SerializeField]
	private uint needResourcesCount = 1;
    [SerializeField]
    private Resource next;
    public Resource Next => next;

 
    [field:SerializeField]
    public Transform BulidPoint { get; private set; }

	protected void Start()
	{
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
