using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : AutoInstanceBehaviour<InfoManager>
{
	[SerializeField]
	private Text infoText = null;
	[SerializeField]
	private GameObject infoObject = null;

	public Text InfoText => infoText;
	public GameObject InfoObject => infoObject;
}
