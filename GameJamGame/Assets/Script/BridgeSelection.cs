﻿using Cinemachine;
using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSelection : MonoBehaviour
{
	public static event EventHandler<SimpleArgs<GameObject>> OnBridgeSelected = delegate { };
	public static Bridge SelectedBridge { get { return _SelectedBridge; } private set { if (_SelectedBridge != value) { _SelectedBridge = value; } OnBridgeSelected.Invoke(null, _SelectedBridge.gameObject); } }
	private static Bridge _SelectedBridge;

	[SerializeField]
	private CinemachineVirtualCamera virtualCam = null;

	[SerializeField]
	private GameObject chooseBridgeUI = null;

	[SerializeField]
	private LeanTweenType cameraLeanTweenType = LeanTweenType.easeInQuad;

	[SerializeField]
	private float maxPoint;

	private float firstSizeValue;

	private IslandEnterTrigger lastIsland = null;
	[SerializeField]
	private float timeAnimation = 2;

	public void Activate(IslandEnterTrigger island)
	{
		lastIsland = island;
		Player.Instance.LastIsland = island;
	}

	public void OnEnable()
	{
		Player.Instance.LockMovement = true;
		firstSizeValue = virtualCam.m_Lens.OrthographicSize;
		LeanTween.value(firstSizeValue, maxPoint, timeAnimation).setOnUpdate((f) => virtualCam.m_Lens.OrthographicSize = f).setEase(cameraLeanTweenType);
		chooseBridgeUI.SetActive(true);
	}

	public void CancelSelection()
	{
		this.gameObject.SetActive(false);
	}

	public void ConfirmSelection()
	{
		if (!this.gameObject.activeSelf)
		{
			return;
		}

		Vector2 cubeRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		RaycastHit2D cubeHit = Physics2D.Raycast(cubeRay, Vector2.zero);

		if (cubeHit.collider != null)
		{
			if (cubeHit.collider.CompareTag("Bridge"))
			{
				SelectedBridge = cubeHit.collider.gameObject.GetComponent<Bridge>();
				lastIsland.Carrier.transform.position = (Vector2)lastIsland.IslandCarrierSpawnPoint.position;
				Spawner.Instance.EnemySpawners = lastIsland.EnemyPoints;

				Destroy(lastIsland.gameObject);

				Resource.RemoveAllLines();
				foreach (Resource item in lastIsland.ResourcesOnIsland)
				{
					item.DrawLine();
				}

				this.gameObject.SetActive(false);
			}
		}
	}

	public void OnDisable()
	{
		LeanTween.cancelAll();
		virtualCam.m_Lens.OrthographicSize = firstSizeValue;
		Player.Instance.LockMovement = false;
		chooseBridgeUI.SetActive(false);
	}
}
