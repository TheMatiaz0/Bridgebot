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

	public bool IsFixed { get; private set; }

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
    private GameObject fullyFixedTrigger = null;

    [SerializeField]
    private BoxCollider2D triggerCollider = null;

    private WorldUI worldUI = null;

    [SerializeField]
    private AudioSource audioSource = null;

    [SerializeField]
    private AudioClip bridgeBuilt = null;

    public string ResourceNumbers ()
    {
        return $"{ResourcesAddedToBuild}/{needResourcesCount}";
    }

    private void RemoveTriggerOnFixed ()
    {
        worldUI?.FirstActivate(false);
        Destroy(triggerCollider);
        // worldUI?.FirstActivate(false);
    }

    protected void OnMouseEnter()
    {
        if (IsFixed)
            return;


        worldUI.FirstActivate(true, ResourceNumbers());
    }


    protected void OnMouseOver()
    {
        if (IsFixed)
        {
            return;
        }

        worldUI.Move(Camera.main.WorldToScreenPoint(this.transform.position));
    }

    protected void OnMouseExit()
    {
        if (IsFixed)
        {
            return;
        }

        worldUI.FirstActivate(false);
    }

    protected new void Awake()
    {
        base.Awake();
        IsFixed = isFixedInit;
        worldUI = GameObject.FindGameObjectWithTag("BridgeUI").GetComponent<WorldUI>();
    }

    protected void Start()
    {
        OnBridgeBuilt += Bridge_OnBridgeBuilt;
        if (IsFixed)
        {
            RemoveTriggerOnFixed();
            return;
        }

        spriteRender.sprite = totallyBroken;
        fullyFixedTrigger.SetActive(false);

       
    }

    private void Bridge_OnBridgeBuilt(object sender, SimpleArgs<Bridge> e)
    {
        audioSource.PlayOneShot(bridgeBuilt);
    }

    private void OnResourceChange (uint currentResources)
    {
        if (IsFixed)
        {
            return;
        }

        worldUI.ResourceCounter.text = ResourceNumbers();

        if (currentResources >= needResourcesCount)
        {
            currentResources = needResourcesCount;
            spriteRender.sprite = fullyFixed;
            OnBridgeBuilt.Invoke(this, this);
            IsFixed = true;
            fullyFixedTrigger.SetActive(true);
            Statistics.Instance.AllBridgeBuilt += 1;
        }

        else if (currentResources >= (needResourcesCount / 2))
        {
            spriteRender.sprite = underConstruction;
        }
    }
}
