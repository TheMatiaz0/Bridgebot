﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cyberevolver.Unity;
using System;
using Cyberevolver;

public class BridgeSelection : AutoInstanceBehaviour<BridgeSelection>
{
	public event EventHandler<SimpleArgs<GameObject>> OnBridgeSelected = delegate { };

	public static GameObject SelectedBridge { get; private set; }

	[SerializeField]
	private CinemachineVirtualCamera virtualCam = null;

	[SerializeField]
	private GameObject chooseBridgeObject = null;

	[SerializeField]
	private LeanTweenType cameraLeanTweenType = LeanTweenType.easeInQuad;

	[SerializeField]
	private float maxPoint;

	private float firstSizeValue;

	private LTDescr valueTween;

	public void OnEnable()
	{
		Player.Instance.LockMovement = true;
		firstSizeValue = virtualCam.m_Lens.OrthographicSize;
		valueTween = LeanTween.value(firstSizeValue, maxPoint, 3).setOnUpdate((f) => virtualCam.m_Lens.OrthographicSize = f).setEase(cameraLeanTweenType);
		chooseBridgeObject.SetActive(true);
	}

	protected void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			this.gameObject.SetActive(false);
			return;
		}

		if (Input.GetMouseButtonDown(0))
		{
			Vector2 cubeRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D cubeHit = Physics2D.Raycast(cubeRay, Vector2.zero);

			if (cubeHit.collider != null)
			{
				if (cubeHit.collider.CompareTag("Bridge"))
				{
					SelectedBridge = cubeHit.collider.gameObject;
					OnBridgeSelected.Invoke(this, SelectedBridge);
					this.gameObject.SetActive(false);
				}
			}
		}
	}

	public void OnDisable()
	{
		LeanTween.cancelAll();
		virtualCam.m_Lens.OrthographicSize = firstSizeValue;
		Player.Instance.LockMovement = false;
		chooseBridgeObject.SetActive(false);
	}
}
