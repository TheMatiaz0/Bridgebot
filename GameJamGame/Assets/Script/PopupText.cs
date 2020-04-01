using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupText : AutoInstanceBehaviour<PopupText>
{
	[SerializeField]
	private Text baseText = null;
	public Text BaseText => baseText;

	[SerializeField]
	private GameObject mainGameObject = null;

	public GameObject MainGameObject => mainGameObject;

}
