using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine.UI;

public class WorldUI : MonoBehaviour
{
	[SerializeField] private GameObject objectToLaunch = null;

	[SerializeField]
	private Text resourceCounter = null;

	public Text ResourceCounter => resourceCounter;

	public void FirstActivate (bool isTrue)
	{
		objectToLaunch.SetActive(isTrue);
	}

	public void Move (Vector2 position)
	{
		objectToLaunch.transform.position = position;
		
	}
}
