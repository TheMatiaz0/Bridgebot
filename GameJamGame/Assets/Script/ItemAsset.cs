using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
[ShowCyberInspector,CreateAssetMenu(fileName ="item",menuName ="item")]
public class ItemAsset : ScriptableObject
{
    [field:SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public Sprite Icon { get; private set; }
}