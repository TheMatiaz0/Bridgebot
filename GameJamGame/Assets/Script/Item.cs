﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
public abstract class Item
{
    public  string Name { get; set; }
    public  Sprite Icon { get; set; }
    public GameObject Prefab { get; set; }
    public virtual void OnStartSelect() { }
    public virtual void OnUse() { }
    public virtual void OnEndSelect() { }

    public Item Apply(ItemAsset asset)
    {
        if (asset is null)
        {
            throw new ArgumentNullException(nameof(asset));
        }
        Name = asset.name;
        Icon = asset.Icon;
        Prefab = asset.Prefab;

        return this;
    }
   
}