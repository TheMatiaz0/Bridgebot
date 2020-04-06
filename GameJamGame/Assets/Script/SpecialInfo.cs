using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialInfo : MonoBehaviourPlus
{
	protected void OnEnable()
	{
     
        this.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
       

    }
    private void Start()
    {
        LeanTween.scale(this.gameObject, Vector3.one, 0.6f).setEaseInOutSine();
    }

    protected void OnDisable()
	{
		// Vector2 vect = new Vector2(200, 0);
		// LeanTween.move(this.gameObject, vect, 3);
	}
}
