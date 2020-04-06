using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceUIActivator
    : MonoBehaviour
{
    public WorldUI ResourceUI { get; private set; } = null;

    protected void Start()
    {
        ResourceUI = GameObject.FindGameObjectWithTag("ResourceUI").GetComponent<WorldUI>();
        ResourceUI.ResourceCounter.text = 0.ToString();
    }

    protected void OnMouseEnter()
    {
        ResourceUI.FirstActivate(true);
    }

    protected void OnMouseOver()
    {
        Vector2 vect = new Vector2(this.transform.position.x, this.transform.position.y + 1.6f);
        ResourceUI.Move(Camera.main.WorldToScreenPoint(vect));
    }

    protected void OnMouseExit()
    {
        ResourceUI.FirstActivate(false);
    }
}
