using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver;
using Cyberevolver.Unity;

public class Statistics : AutoInstanceBehaviour<Statistics>
{
	public Cint AllBridgeBuilt { get; set; }

	public Cint AllKilledEnemies { get; set; }

	public string GetStats()
	{
		return $"Bridges built: {AllBridgeBuilt}\nEnemies defeated: {AllKilledEnemies}";
	}
}
