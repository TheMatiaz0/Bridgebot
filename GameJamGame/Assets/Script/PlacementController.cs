using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver;

public class PlacementController : AutoInstanceBehaviour<PlacementController>
{
	private GameObject currentObject = null;
	private SpriteRenderer spriteRender = null;
	private Collider2D col2D = null;

	public bool CanBuild { get; private set; }

	public void Activate (GameObject prefab)
	{
		// delay?
		currentObject = Instantiate(prefab,	Vector2.zero, Quaternion.identity);
		spriteRender = currentObject.GetComponent<SpriteRenderer>();
		col2D = currentObject.GetComponent<Collider2D>();
		col2D.isTrigger = true;
		spriteRender.color = Color.red;
	}

	protected void Update()
	{
		if (currentObject == null)
		{
			return;
		}

		currentObject.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

		RaycastHit2D hit = currentObject.Ray2DWithoutThis(currentObject.transform.position, Vector2.zero, 15);
		if (hit.collider != null)
		{ 
			if (hit.collider.CompareTag("BuildingPlace"))
			{
				spriteRender.color = Color.green;
				CanBuild = true;
				hit.collider.tag = "";
				return;
			}
		}

		spriteRender.color = Color.red;
		CanBuild = false;
	}

	public void OnPlace ()
	{
		spriteRender.color = Color.white;
		col2D.isTrigger = false;
		currentObject = null;
	}

	public void Deactive ()
	{
		Destroy(currentObject);
	}
}
