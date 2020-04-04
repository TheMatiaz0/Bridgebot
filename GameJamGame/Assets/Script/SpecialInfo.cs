using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialInfo : MonoBehaviourPlus
{
	protected void OnEnable()
	{
		Invoke((m) => this.gameObject.SetActive(false), 4);
		// LeanTween.(this.gameObject, vect, 3).setOnComplete(() => this.gameObject.SetActive(false));
	}

	protected void OnDisable()
	{
		// Vector2 vect = new Vector2(200, 0);
		// LeanTween.move(this.gameObject, vect, 3);
	}
}
