using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
public sealed class Equipment : AutoInstanceBehaviour<Equipment>
{
    public class ItemAddedArg:EventArgs
    {
        public Item Item { get; }
        public Equipment Equipment { get; }
      
        public ItemAddedArg(Item item, Equipment equipment)
        {
            Item = item ?? throw new ArgumentNullException(nameof(item));
            Equipment = equipment ?? throw new ArgumentNullException(nameof(equipment));
        }
    }
    protected const string PREFAB = "Prefabs";
    private const string REFERENCE = "REFERENCE";
    [SerializeField, AssetOnly,BoxGroup(PREFAB)]
    private HotBarElement hotBarPrefab;
    [SerializeField, AssetOnly, BoxGroup(REFERENCE),RequiresAny]
    private Transform eqParent;
    private readonly List<Item> items = new List<Item>();

    public event EventHandler<ItemAddedArg> OnItemAdded = delegate { };
  
    public void AddItem(Item item)
    {
        if (items.Contains(item) == false)
        {
            items.Add(item);
            RefreshGroup();
            OnItemAdded(this, new ItemAddedArg(item, this));

        }
           
    }
    public void RefreshGroup()
    {
        eqParent.KillAllChild();
        foreach(var item in items)
        {
            var element = Instantiate(hotBarPrefab);
            element.transform.SetParent(eqParent);
            element.Item = item;
        }
        
    }

}