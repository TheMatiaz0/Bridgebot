using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialInfo : MonoBehaviourPlus
{
    private void Start()
    {
        LeanTween.scale(this.gameObject, Vector3.one, 0.6f).setEaseInOutSine();
        Invoke((m) => this.gameObject.SetActive(false), TimeSpan.FromSeconds(5));
    }
}
