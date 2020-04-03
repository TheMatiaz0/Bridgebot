using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public sealed class Equipment : AutoInstanceBehaviour<Equipment>
{
    public class ItemAddedArg : EventArgs
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
    private const string REFERENCE = "Reference";
    private const string VALUES = "Values";
    [SerializeField, BoxGroup(REFERENCE), RequiresAny]
    private Transform eqParent;
    [SerializeField, BoxGroup(VALUES)]
    private SerializedTimeSpan pistolDelay = TimeSpan.FromSeconds(0.3f);
    private readonly List<Item> items = new List<Item>();
    [SerializeField, AssetOnly]
    private ItemAsset pistol;
    [SerializeField, AssetOnly]
    private ItemAsset turret;
    private Cint selected = 0;
    private HotBarElement[] elements = null;
    public event EventHandler<ItemAddedArg> OnItemAdded = delegate { };

    public void AddItem(Item item)
    {
        if (items.Contains(item) == false)
        {
            items.Add(item);
            RefreshGroup();
            OnItemAdded(this, new ItemAddedArg(item, this));
            if (items.Count - 1 == selected)
                item.OnStartSelect();

        }

    }

    protected override void Awake()
    {
        base.Awake();
        RefreshGroup();

    }
    private void Start()
    {
        AddItem(new Pistol(pistolDelay.TimeSpan).Apply(pistol));
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
            Select(0);
        if (Input.GetKey(KeyCode.Alpha2))
            Select(1);
        if (Input.GetKey(KeyCode.Alpha3))
            Select(2);
    }
    public Item GetCurrent()
    {
        if (selected >= items.Count)
        {
            return null;
        }
        else
            return items[(int)(uint)selected];
    }
    public void Select(Cint index)
    {
        if (index == selected)
            return;
        selected = index;

        RefreshSelect();
    }

    private void RefreshSelect()
    {
        for (int i = 0; i < 3; i++)
        {
            elements[i].SetSelect(selected == i);
        }

    }
    private void RefreshGroup()
    {
        int i = 0;
        elements = new HotBarElement[3];
        foreach (Transform gameObj in eqParent.GetAllChildren())
        {
            HotBarElement hotBarItem = gameObj.GetComponent<HotBarElement>();
            if (i < items.Count)
            {
                hotBarItem.SetItem(items[i]);
            }
            else
                hotBarItem.SetItem(null);

            elements[i] = hotBarItem;
            i++;
        }
        RefreshSelect();

    }

}