using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeWalkIn : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		collision.gameObject.layer = 10;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		collision.gameObject.layer = 0;
	}
}
