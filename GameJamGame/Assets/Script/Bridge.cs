using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cyberevolver.Unity;
using Cyberevolver;
using System;

public class Bridge : MonoBehaviourPlus
{
    public static Cint AllBridgeBuilt { get; set; }

    [SerializeField]
    private bool isFixedInit = false;

	public bool IsFixed { get { return _IsFixed; } private set { if (_IsFixed != value) { _IsFixed = value; RemoveTriggerOnFixed(); } } }
    private bool _IsFixed;

    public static event EventHandler<SimpleArgs<Bridge>> OnBridgeBuilt = delegate { };

	[SerializeField]
	private uint needResourcesCount = 10;

    public uint ResourcesAddedToBuild { get { return _ResourcesAddedToBuild; } set { if (_ResourcesAddedToBuild != value) _ResourcesAddedToBuild = value; OnResourceChange(_ResourcesAddedToBuild); } }
    private uint _ResourcesAddedToBuild;

    [SerializeField]
    private SpriteRenderer spriteRender = null;

    [SerializeField]
    private Sprite fullyFixed = null;
    [SerializeField]
    private Sprite totallyBroken = null;
    [SerializeField]
    private Sprite underConstruction = null;

    [SerializeField]
    private GameObject fullyFixedTrigger;

    [SerializeField]
    private BoxCollider2D triggerCollider = null;

    public string ResourceNumbers ()
    {
        return $"{ResourcesAddedToBuild}/{needResourcesCount}";
    }

    private void RemoveTriggerOnFixed ()
    {
        Destroy(triggerCollider);
        WorldUI.Instance?.FirstActivate(false);
    }

    protected void OnMouseEnter()
    {
        if (IsFixed)
            return;

        WorldUI.Instance.FirstActivate(true);
    }


    protected void OnMouseOver()
    {
        if (IsFixed)
        {
            return;
        }

        WorldUI.Instance.Move(Camera.main.WorldToScreenPoint(this.transform.position));
    }

    protected void OnMouseExit()
    {
        if (IsFixed)
        {
            return;
        }

        WorldUI.Instance.FirstActivate(false);
    }

    protected new void Awake()
    {
        base.Awake();
        IsFixed = isFixedInit;
    }

    protected void Start()
    {
        if (IsFixed)
        {
            return;
        }

        spriteRender.sprite = totallyBroken;
        fullyFixedTrigger.SetActive(false);

        WorldUI.Instance.ResourceCounter.text = ResourceNumbers();
    }

    private void OnResourceChange (uint currentResources)
    {
        if (IsFixed)
        {
            return;
        }

        WorldUI.Instance.ResourceCounter.text = ResourceNumbers();

        if (currentResources >= needResourcesCount)
        {
            currentResources = needResourcesCount;
            spriteRender.sprite = fullyFixed;
            OnBridgeBuilt.Invoke(this, this);
            IsFixed = true;
            fullyFixedTrigger.SetActive(true);
            AllBridgeBuilt += 1;
        }

        else if (currentResources >= (needResourcesCount / 2))
        {
            spriteRender.sprite = underConstruction;
        }
    }
}
