using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;

[ShowCyberInspector]
public class Resource : MonoBehaviourPlus
{
    public static List<Resource> ResourceList { get; private set; } = new List<Resource>();

    [field: SerializeField]
    public Transform[] Points { get; private set; }

    [SerializeField]
    private GameObject pavementPrefab = null;

    [SerializeField]
    private uint resourceCount = 10;

    public uint ResourceCount { get { return _ResourceCount; } set { if (_ResourceCount <= 0) { Destroy(this.gameObject); } _ResourceCount = value; } }
    private uint _ResourceCount = 10;

    protected void Start()
    {
        ResourceCount = resourceCount;
        ResourceList.Add(this);

        GameObject line = Instantiate(pavementPrefab);

        LineRenderer lineRender = line.GetComponent<LineRenderer>();

        lineRender.positionCount = Points.Length + 1;

        lineRender.SetPosition(0, (Vector2)this.transform.position);

        for (int x = 1; x < Points.Length + 1; x++)
        {
            lineRender.SetPosition(x, (Vector2)Points[x - 1].position);
        }
    }

    private void OnEnable()
    {
        BridgeSelection.OnBridgeSelected += BridgeSelection_OnBridgeSelected;
    }

    protected void OnDisable()
    {
        BridgeSelection.OnBridgeSelected -= BridgeSelection_OnBridgeSelected;
    }

    private void BridgeSelection_OnBridgeSelected(object sender, SimpleArgs<GameObject> e)
    {
    }

    private void OnDrawGizmos()
    {
        try
        {
            Gizmos.DrawLine(this.transform.position, Points[0].position);
            for (int x = 0; x < Points.Length - 1; x++)
            {
                Gizmos.DrawLine(Points[x].position, Points[x + 1].position);
            }
        }
        catch { }
       
    }

    public static Resource GetClosestResource(Vector2 currentPosition, float range)
    {
        Resource bestTarget = null;
        foreach (Resource resource in ResourceList)
        {
            if (resource == null)
            {
                continue;
            }

            Vector2 directionToTarget = (Vector2)resource.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < range)
            {
                range = dSqrToTarget;
                bestTarget = resource;
            }
        }

        return bestTarget;
    }
}