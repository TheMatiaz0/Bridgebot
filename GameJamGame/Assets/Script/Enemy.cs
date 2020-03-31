using Cyberevolver;
using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Enemy : MonoBehaviour, IHpable
{
	[SerializeField]
	private Cint startHp = 10;

	[SerializeField]
	private Rigidbody2D rb2D = null;

	[SerializeField]
	private float speed = 10;

	private Transform targetTransform = null;

	[SerializeField]
	private Seeker seeker;

	[SerializeField]
	private Rigidbody2D Rigidbody2D;

	public Path Path { get; private set; }

	private float nextWaypointDistance = 3;

	private int currentWaypoint = 0;
	private bool reachedEndOfPath;

	[SerializeField, ResetButton(1)]
	private float MoveSize = 1;

	private Cyberevolver.Unity.CooldownController moveCooldown;

	[SerializeField]
	private SerializedTimeSpan jumpDelay = TimeSpan.FromSeconds(1);

	public Team CurrentTeam { get; private set; } = Team.Bad;

	public Hp Hp { get; private set; }


	protected void Awake()
	{
		moveCooldown = new CooldownController(this, jumpDelay.TimeSpan);
		Hp = new Hp(startHp, 0, startHp);
		Hp.OnValueChangeToMin += Hp_OnValueChangeToMin;
	}

	private void Hp_OnValueChangeToMin(object sender, Hp.HpChangedArgs e)
	{
		Destroy(this.gameObject);
	}

	protected void Start()
	{
		targetTransform = Player.Instance.transform;
		seeker.StartPath(transform.position, targetTransform.position, OnPathComplete);
	}

	public void OnPathComplete(Path p)
	{
		Debug.Log("A path was calculated. Did it fail with an error? " + p.error);

		if (!p.error)
		{
			Path = p;
			// Reset the waypoint counter so that we start to move towards the first point in the path
			currentWaypoint = 0;
		}
	}

	public void Update()
	{
		if (Path == null)
		{
			// We have no path to follow yet, so don't do anything
			return;
		}

		// Check in a loop if we are close enough to the current waypoint to switch to the next one.
		// We do this in a loop because many waypoints might be close to each other and we may reach
		// several of them in the same frame.
		reachedEndOfPath = false;
		// The distance to the next waypoint in the path
		float distanceToWaypoint;
		while (true)
		{
			// If you want maximum performance you can check the squared distance instead to get rid of a
			// square root calculation. But that is outside the scope of this tutorial.
			distanceToWaypoint = Vector3.Distance(transform.position, Path.vectorPath[currentWaypoint]);
			if (distanceToWaypoint < nextWaypointDistance)
			{
				// Check if there is another waypoint or if we have reached the end of the path
				if (currentWaypoint + 1 < Path.vectorPath.Count)
				{
					currentWaypoint++;
				}
				else
				{
					// Set a status variable to indicate that the agent has reached the end of the path.
					// You can use this to trigger some special code if your game requires that.
					reachedEndOfPath = true;
					break;
				}
			}
			else
			{
				break;
			}
		}

		// Slow down smoothly upon approaching the end of the path
		// This value will smoothly go from 1 to 0 as the agent approaches the last waypoint in the path.
		// var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;

		// Direction to the next waypoint
		// Normalize it so that it has a length of 1 world unit
		Direction dir = ((Vector2)(Path.vectorPath[currentWaypoint] - transform.position)).normalized;

		if (moveCooldown.Try())
		{
			this.Rigidbody2D.MovePosition(this.transform.Get2DPos() + dir.ToVector2() * MoveSize);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (HpableExtension.IsFromWrongTeam(this, collision, out Bullet bullet))
		{
			this.Hp.TakeHp(bullet.Dmg, "Bullet");
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
	}
}
