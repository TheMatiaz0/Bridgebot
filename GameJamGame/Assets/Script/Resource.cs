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

    [field: SerializeField]
    public Transform[] Points { get; private set; }

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
}