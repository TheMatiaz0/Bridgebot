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

    public uint CurrentResources { get; private set; }

    public Team CurrentTeam { get; private set; } = Team.Good;

    public Hp Hp { get; private set; }
    [Auto]
    public AIPath AIPath { get; private set; }

    public Resource Current { get; private set; }

    [SerializeField]
    private uint startMaxHp;
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

    protected void Start()
    {
        Hp = new Hp(startMaxHp, 0, startMaxHp);
        Hp.OnValueChangeToMin += Hp_OnValueChangeToMin;
        Hp.OnValueChanged += Hp_OnValueChanged;

    }

    private void Hp_OnValueChanged(object sender, Hp.HpChangedArgs e)
    {

    }

    protected void OnEnable()
    {
        PhaseController.Instance.OnPhaseChanged += Instance_OnPhaseChanged;
        Bridge.OnBridgeBuilt += Bridge_OnBridgeBuilt;
    }

    private void Bridge_OnBridgeBuilt(object sender, Cyberevolver.SimpleArgs<Bridge> e)
    {
        IsLaunched = false;
        StopAllCoroutines();
    }

    protected void OnDisable()
    {
        PhaseController.Instance.OnPhaseChanged -= Instance_OnPhaseChanged;
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
        if (AIPath.destination.x > 1)
        {
            spriteRender.flipX = true;
        }

        else if (AIPath.destination.x < -1)
        {
            spriteRender.flipX = false;
        }

        if (currentTarget == null)
        {
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        //rb2D.MovePosition((Vector2)transform.position + (Vector2)currentTarget.position * speed * Time.fixedDeltaTime);
    }

    private IEnumerator Run()
    {
        while (true)
        {
            yield return GoToResource();

            // gather resources
            yield return GatherResources(Current);
            woodSpriteRender.sprite = fullWood;

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
        Current = Resource.GetClosestResource(this.transform.position, 40);

        if (Current == null)
        {
            throw new Exception("No resource to gather.");
        }

        AIPath.canMove = true;
        AIPath.canSearch = true;
        AIPath.destination = Current.transform.position;
        yield return Async.NextFrame;
        yield return Async.Until(() => AIPath.reachedEndOfPath);

        /*
        if (Current != null)
        {

        }
            else
            {
                yield return StartCoroutine(GoToResource());
            }
            */
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
            Debug.Log("Coming to point...");
            yield return Async.Until(() => Vector2.Distance(this.transform.position, item.position) <= 0.2f);
        }

        currentTarget = null;
    }

    private IEnumerator GatherResources(Resource resource)
    {
        while (CurrentResources < maxCapacity && Current != null)
        {
            resource.ResourceCount -= 1;
            CurrentResources += 1;
            Debug.Log($"I have {CurrentResources} resources now");

            yield return Async.Wait(TimeSpan.FromMilliseconds(600));
        }

        yield return StartCoroutine(GoToResource());
    }

    private IEnumerator FixBridge(Bridge bridge)
    {
        while (CurrentResources > 0)
        {
            bridge.ResourcesAddedToBuild += 1;
            CurrentResources -= 1;
            Debug.Log($"I added to the bridge {bridge.ResourcesAddedToBuild} resources. I have {CurrentResources} now");

            yield return Async.Wait(TimeSpan.FromMilliseconds(900));
        }

        woodSpriteRender.sprite = null;
        Debug.Log("O cholibka, skończyły mi się surowce.");

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
        // game over!
        Player.Instance.LaunchGameOver();
    }


}
