﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandEnterTrigger : MonoBehaviour
{
	private bool hasActivated = false;

	[SerializeField]
	private BridgeSelection selection;

	private void OnTriggerEnter2D(Collider2D collision)
	{

		if (collision.GetComponent<Player>())
		{
			if (hasActivated == false)
			{
				ActivateBridgeSelection();
				hasActivated = true;
				return;
			}

			else if (hasActivated == true && selection.gameObject.activeSelf == false)
			{
				PopupText.Instance.MainGameObject.SetActive(true);
				PopupText.Instance.BaseText.text = "Press 'F' to choose a bridge.";
			}

		}
	}

	private void ActivateBridgeSelection ()
	{
		selection.gameObject.SetActive(true);
		selection.Activate(this);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		PopupText.Instance?.MainGameObject?.SetActive(false);
	}

	protected void Update()
	{
		if (hasActivated == false || selection.gameObject.activeSelf == true)
		{
			return;
		}

		if (Input.GetKeyDown(KeyCode.F))
		{
			ActivateBridgeSelection();
			PopupText.Instance?.MainGameObject?.SetActive(false);
		}
	}
}
