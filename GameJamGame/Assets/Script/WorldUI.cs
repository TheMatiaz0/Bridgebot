using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver;
using Cyberevolver.Unity;

public class WorldUI : AutoInstanceBehaviour<WorldUI>
{
	[SerializeField] private GameObject objectToLaunch = null;


	public void FirstActivate (bool isTrue)
	{
		objectToLaunch.SetActive(isTrue);
	}

	public void Move (Vector2 position)
	{
		objectToLaunch.transform.position = position;
		
	}
}
