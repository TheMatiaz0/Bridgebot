using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cyberevolver.Unity;
using System;
using Cyberevolver;

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

	public void Activate (IslandEnterTrigger island)
	{
		lastIsland = island;
	}

	public void OnEnable()
	{
		Player.Instance.LockMovement = true;
		firstSizeValue = virtualCam.m_Lens.OrthographicSize;
		LeanTween.value(firstSizeValue, maxPoint, 3).setOnUpdate((f) => virtualCam.m_Lens.OrthographicSize = f).setEase(cameraLeanTweenType);
		chooseBridgeUI.SetActive(true);
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
					SelectedBridge = cubeHit.collider.gameObject.GetComponent<Bridge>();
					this.gameObject.SetActive(false);
					lastIsland.Carrier.transform.position = (Vector2)lastIsland.IslandCarrierSpawnPoint.position;
					Destroy(lastIsland.gameObject);
				}
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
