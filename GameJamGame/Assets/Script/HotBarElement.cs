using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HotBarElement : Component
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Text text;
    public Item Item { get; set; }
}