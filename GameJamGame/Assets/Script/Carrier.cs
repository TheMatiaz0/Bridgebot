using Cyberevolver;
using Cyberevolver.Unity;
using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Carrier : MonoBehaviourPlus, IHpable
{
    [SerializeField]
    private uint maxCapacity = 8;

    public Cint CurrentResources { get { return _CurrentResources; } private set { if (_CurrentResources != value) _CurrentResources = value; OnResourceChange(_CurrentResources); } }
    private Cint _CurrentResources;

    public Team CurrentTeam { get; private set; } = Team.Good;

    public Hp Hp { get; private set; }
    [Auto]
    public AIPath AIPath { get; private set; }

    public Resource Current { get; private set; }

    [SerializeField]
    private uint startMaxHp = 10;
    public bool IsLaunched { get; private set; }

    private Coroutine logic = null;

    private Bridge selectedBridge;

    [SerializeField]
    private float speed = 10;

    private Transform currentTarget = null;

    [SerializeField]
    private SpriteRenderer spriteRender = null;
    [SerializeField]
    private SpriteRenderer woodSpriteRender = null;

    [SerializeField]
    private Sprite fullWood = null;

    [SerializeField]
    private float gatherRange = 40;

    [SerializeField]
    private float distanceBetweenPoints = 0.2f;

    [SerializeField]
    private SerializedTimeSpan gatherCooldown;

    [SerializeField]
    private SerializedTimeSpan fixCooldown;

    [Auto]
    public Animator Animator { get; private set; }
    [SerializeField]
    private GameObject dmgEffect;
    [SerializeField]
    private Color coloringColor = Color.red;

    [SerializeField]
    private HpManager hpManager = null;

    private WorldUI carrierUI = null;
    private bool isGoingToResource;

    [SerializeField]
    private AudioClip gatheringSound = null;

    [SerializeField]
    private AudioSource audioSource = null;

    private void OnResourceChange(uint newValue)
    {
        if (carrierUI != null)
            carrierUI.ResourceCounter.text = newValue.ToString();
    }

    protected void Start()
    {
        Hp = new Hp(startMaxHp, 0, startMaxHp);
        hpManager.CurHealth = Hp;
        Hp.OnValueChangeToMin += Hp_OnValueChangeToMin;
        Hp.OnValueChanged += Hp_OnValueChanged;
        hpManager.Refresh();
        PhaseController.Instance.OnPhaseChanged += Instance_OnPhaseChanged;
        carrierUI = GameObject.FindGameObjectWithTag("CarrierUI").GetComponent<WorldUI>();

    }

    private void Hp_OnValueChanged(object sender, Hp.HpChangedArgs e)
    {
        if (dmgEffect != null)
        {
            Instantiate(dmgEffect).transform.position = this.transform.position;
            LeanTween.cancel(this.gameObject);
            LeanTween.color(this.gameObject, Color.red, 1f)
                .setOnComplete(() => LeanTween.color(this.gameObject, Color.white, 1f));
        }

    }

    protected void OnEnable()
    {
        Bridge.OnBridgeBuilt += Bridge_OnBridgeBuilt;
    }

    private void Bridge_OnBridgeBuilt(object sender, Cyberevolver.SimpleArgs<Bridge> e)
    {
        IsLaunched = false;
        CurrentResources = 0;
        woodSpriteRender.sprite = null;
        StopAllCoroutines();
    }

    protected void OnDisable()
    {
        Bridge.OnBridgeBuilt -= Bridge_OnBridgeBuilt;
    }

    private void Instance_OnPhaseChanged(object sender, PhaseController.Phase e)
    {
        switch (e)
        {
            case PhaseController.Phase.FIGHTING:
                Launch();
                break;
        }
    }

    // [Button]
    public bool Launch()
    {
        if (IsLaunched)
            return false;
        if (BridgeSelection.SelectedBridge is null || BridgeSelection.SelectedBridge.IsFixed)
            return false;
        IsLaunched = true;
        selectedBridge = BridgeSelection.SelectedBridge;
        logic = StartCoroutine(Run());

        return true;
    }

    protected void Update()
    {
        if (currentTarget == null)
        {
            Animator.SetBool("Walk", false);
            return;
        }

        if (currentTarget.position.x > 1)
        {
            spriteRender.flipX = true;
        }

        else if (currentTarget.position.x < -1)
        {
            spriteRender.flipX = false;
        }

        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
        Animator.SetBool("Walk", true);

        //rb2D.MovePosition((Vector2)transform.position + (Vector2)currentTarget.position * speed * Time.fixedDeltaTime);
    }

    protected void OnMouseEnter()
    {
        carrierUI.FirstActivate(true, CurrentResources.ToString());
        // WorldUI.Instance.FirstActivate(true);
    }

    protected void OnMouseOver()
    {
        Vector2 vect = new Vector2(this.transform.position.x, this.transform.position.y + 1.3f);
        carrierUI.Move(Camera.main.WorldToScreenPoint(vect));
        // WorldUI.Instance.Move(Camera.main.WorldToScreenPoint(vect));
    }

    protected void OnMouseExit()
    {
        carrierUI.FirstActivate(false);
        // WorldUI.Instance.FirstActivate(false);
    }

    private IEnumerator Run()
    {
        while (true)
        {
            isGoingToResource = true;

            yield return GoToResource();
            isGoingToResource = false;

            // gather resources

            Animator.SetBool("ChopChop", true);
            audioSource.PlayOneShot(gatheringSound);
            yield return GatherResources(Current);
            woodSpriteRender.sprite = fullWood;
            Animator.SetBool("ChopChop", false);
            audioSource.Stop();

            yield return GoPoints();

            // fix the bridge

            yield return FixBridge(selectedBridge);


        }
    }

    /// <summary>
    /// Pathfinding method.
    /// </summary>
    /// <returns></returns>
    private IEnumerator GoToResource()
    {
        Current = Resource.GetClosestResource(this.transform.position, gatherRange);
        currentTarget = Current.transform;

        if (Current == null)
        {
            throw new Exception("No resource to gather.");
        }

        AIPath.canMove = true;
        AIPath.canSearch = true;
        AIPath.destination = Current.transform.position;
        yield return Async.NextFrame;
        yield return Async.Until(() => AIPath.reachedEndOfPath);
    }

    private IEnumerator GoPoints(bool reverse = false)
    {
        AIPath.canMove = false;
        AIPath.canSearch = false;

        Transform[] transforms = (Transform[])Current.Points.Clone();

        if (reverse)
        {
            Array.Reverse(transforms);
        }

        foreach (Transform item in transforms)
        {
            currentTarget = item;
            // Debug.Log("Coming to point...");
            yield return Async.Until(() => Vector2.Distance(this.transform.position, item.position) <= distanceBetweenPoints);
        }

        currentTarget = null;
    }

    private IEnumerator GatherResources(Resource resource)
    {
        while (CurrentResources < maxCapacity && Current != null)
        {
            resource.ResourceCount -= 1;
            CurrentResources += 1;

            yield return Async.Wait(gatherCooldown.TimeSpan);
        }

        yield return StartCoroutine(GoToResource());
    }

    private IEnumerator FixBridge(Bridge bridge)
    {

        while (CurrentResources > 0)
        {
            Animator.SetBool("ChopChop", true);
            bridge.ResourcesAddedToBuild += 1;
            CurrentResources -= 1;

            yield return Async.Wait(fixCooldown.TimeSpan);
            Animator.SetBool("ChopChop", false);
        }

        woodSpriteRender.sprite = null;

        yield return GoPoints(true);

        yield break;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (HpableExtension.IsFromWrongTeam(this, collision, out Bullet bullet))
        {
            this.Hp.TakeHp(bullet.Dmg, "Bullet");
            bullet.Kill();
        }
    }

    private void Hp_OnValueChangeToMin(object sender, Hp.HpChangedArgs e)
    {
        Player.Instance.LaunchGameOver();
    }


}
