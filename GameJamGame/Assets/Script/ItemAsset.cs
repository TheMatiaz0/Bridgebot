using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[ShowCyberInspector, CreateAssetMenu(fileName = "item", menuName = "item")]
public class ItemAsset : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public Sprite Icon { get; private set; }

    [field: SerializeField]
    public GameObject prefab { get; private set; }
}