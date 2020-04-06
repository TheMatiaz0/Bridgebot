using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver;

public class PlacementController : AutoInstanceBehaviour<PlacementController>
{
	private GameObject currentObject = null;
	private GameObject lastObject = null;
	private SpriteRenderer spriteRender = null;
	private Collider2D col2D = null;
	private Building building = null;

	private RaycastHit2D hit;

	public bool CanBuild { get; private set; }

	public void Activate (GameObject prefab)
	{
		Deactive();
		// delay?
		currentObject = Instantiate(prefab,	Vector2.zero, Quaternion.identity);
		spriteRender = currentObject.GetComponent<SpriteRenderer>();
		col2D = currentObject.GetComponent<Collider2D>();
		building = currentObject.GetComponent<Building>();
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

		hit = currentObject.Ray2DWithoutThis(currentObject.transform.position, Vector2.zero, 15);
		if (hit.collider != null)
		{ 
			if (hit.collider.CompareTag("BuildingPlace"))
			{
				spriteRender.color = Color.green;
				CanBuild = true;
				return;
			}
		}

		spriteRender.color = Color.red;
		CanBuild = false;
	}

	public void OnPlace ()
	{
		building.OnPlace();
		spriteRender.color = Color.white;
		hit.collider.tag = "Untagged";
		col2D.isTrigger = false;
		currentObject = null;
	}

	public void Deactive ()
	{
		if (lastObject == currentObject)
		{
			return;
		}
		Destroy(currentObject);
	}
}
