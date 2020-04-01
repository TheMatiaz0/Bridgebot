using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandEnterTrigger : MonoBehaviour
{
	[SerializeField]
	private BridgeSelection bridgeSelection = null;

	private bool hasActivated = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{

		if (collision.GetComponent<Player>())
		{
			if (hasActivated == false)
			{
				bridgeSelection.gameObject.SetActive(true);
				hasActivated = true;
			}

			else
			{
				PopupText.Instance.MainGameObject.SetActive(true);
				PopupText.Instance.BaseText.text = "Press 'F' to choose a bridge.";
			}

		}
	}

	protected void Update()
	{
		if (hasActivated == false && bridgeSelection.gameObject.activeSelf == true)
		{
			return;
		}

		if (Input.GetKeyDown(KeyCode.F))
			bridgeSelection.gameObject.SetActive(true);
	}
}
