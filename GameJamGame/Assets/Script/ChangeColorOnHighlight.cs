using Cyberevolver;
using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeColorOnHighlight : CanvasBehaviour
{
	[SerializeField]
	private Text textToChange = null;

	[SerializeField]
	private Color nonSelectColor = Color.gray;

	[SerializeField]
	private Color selectColor = Color.black;

	[SerializeField]
	private float timeToFade = 2;


	protected override void PointerGuiAreaEnter(BaseEventData data)
	{
		base.PointerGuiAreaEnter(data);

		LeanTween.colorText(textToChange.rectTransform, selectColor, timeToFade);
		// textToChange.color = selectColor;
	}

	protected override void PointerGuiAreaExit(BaseEventData data)
	{
		base.PointerGuiAreaExit(data);
		// textToChange.color = nonSelectColor;
		LeanTween.colorText(textToChange.rectTransform, nonSelectColor, timeToFade);
	}
}
