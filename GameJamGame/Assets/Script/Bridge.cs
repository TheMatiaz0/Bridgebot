using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cyberevolver.Unity;
using Cyberevolver;
using System;

public class Bridge : MonoBehaviourPlus
{

    [SerializeField]
    private bool isFixedInit = false;

	public bool IsFixed { get { return _IsFixed; } private set { if (_IsFixed != value) { _IsFixed = value; RemoveTriggerOnFixed(); } } }
    private bool _IsFixed;

    public event EventHandler<SimpleArgs<Bridge>> OnBridgeBuilt = delegate { };

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

    private void RemoveTriggerOnFixed ()
    {
        Destroy(triggerCollider);
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
    }

    private void OnResourceChange (uint currentResources)
    {
        if (IsFixed)
        {
            return;
        }

        if (currentResources >= needResourcesCount)
        {
            currentResources = needResourcesCount;
            spriteRender.sprite = fullyFixed;
            OnBridgeBuilt.Invoke(this, this);
            IsFixed = true;
            fullyFixedTrigger.SetActive(true);
        }

        else if (currentResources >= (needResourcesCount / 2))
        {
            spriteRender.sprite = underConstruction;
        }
    }
}
