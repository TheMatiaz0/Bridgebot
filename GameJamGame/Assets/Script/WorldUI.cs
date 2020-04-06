using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private void Start()
    {
        Player.Instance.OnCorrectEnds += Instance_OnCorrectEnds;
    }

    private void Instance_OnCorrectEnds(object sender, EventArgs e)
    {
        Destroy(objectToLaunch.gameObject);
    }

	protected virtual void OnDestroy()
	{
		if(Player.Instance)
			Player.Instance.OnCorrectEnds -= Instance_OnCorrectEnds;
	}

    public void FirstActivate(bool isTrue, string val = "")
    {
        if (objectToLaunch == false)
            return;
        if (Time.timeScale == 0 && isTrue)
            return;
        ResourceCounter.text = val;
        LeanTween.cancel(objectToLaunch.gameObject);
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
             .setOnComplete(() => { objectToLaunch.SetActive(false); deactiving = false; });

        }

    }

    public void Move(Vector2 position)
    {

        if (deactiving || objectToLaunch == null)
            return;

        objectToLaunch.transform.position = position;

    }
}
