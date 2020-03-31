using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandEnterTrigger : MonoBehaviour
{
	[SerializeField]
	private BridgeSelection bridgeSelection = null;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<Player>())
		{
			bridgeSelection.gameObject.SetActive(true);
		}
	}
}
