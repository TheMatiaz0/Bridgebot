using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver;

public class PlacementController : AutoInstanceBehaviour<PlacementController>
{
	private GameObject currentObject = null;
	private SpriteRenderer spriteRender = null;

	public void Activate (GameObject prefab)
	{
		// delay?
		currentObject = Instantiate(prefab,	Vector2.zero, Quaternion.identity);
		spriteRender = currentObject.GetComponent<SpriteRenderer>();
	}

	protected void Update()
	{
		if (currentObject == null)
		{
			return;
		}

		currentObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		RaycastHit2D hit = this.gameObject.Ray2DWithoutThis(currentObject.transform.position, Vector2.zero, 45f);
		if (hit.transform.tag == "BuildingPlace")
		{
			spriteRender.color = Color.green;
		}

		else
		{
			spriteRender.color = Color.red;
		}
	}

	public void OnPlace ()
	{
		spriteRender.color = Color.white;
	}

	public void Deactive ()
	{
		Destroy(currentObject);
	}
}
