using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine.UI;
using System;

public class WorldUI : MonoBehaviour
{
	[SerializeField] private GameObject objectToLaunch = null;

	[SerializeField]
	private Text resourceCounter = null;
    [SerializeField]
    private LeanTweenType anim = LeanTweenType.punch;

	public Text ResourceCounter => resourceCounter;

    bool deactiving = false;
    private void Awake()
    {
        this.transform.localScale = new Vector2(0.1f, 0.1f);
    }
    public void FirstActivate (bool isTrue)
	{
        LeanTween.cancel(this.gameObject);
        deactiving = false;
        if (isTrue)
        {
           
            objectToLaunch.SetActive(true);
            LeanTween.scale(objectToLaunch.gameObject, new Vector2(1, 1), 0.15f).setEase(anim);
        }
          
        else
        {
            deactiving = true;
            LeanTween.scale(objectToLaunch.gameObject, new Vector3(0.1f, 0.1f), 0.15f).setEase(anim)
             .setOnComplete(() => { objectToLaunch.SetActive(false);deactiving = false; });

        }

    }

	public void Move (Vector2 position)
	{
        if (deactiving == false)
            objectToLaunch.transform.position = position;
		
	}
}
