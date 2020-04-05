using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver;
using Cyberevolver.Unity;

public class Statistics : AutoInstanceBehaviour<Statistics>
{
	public Cint AllBridgeBuilt { get; set; }

	public List<Enemy> AllKilledEnemies { get; set; } = new List<Enemy>();
}
