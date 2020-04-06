using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver;
using Cyberevolver.Unity;
using System;
using TypeReferences;
using System.Reflection;

public class ItemOnTrigger : MonoBehaviourPlus
{
	[SerializeField]
	private ItemAsset itemAsset = null;

	[ClassExtends(typeof(Item))]
	public ClassTypeReference itemClass;


	protected void OnTriggerEnter2D(Collider2D collision)
	{
		PopupText.Instance.MainGameObject.SetActive(true);
		PopupText.Instance.BaseText.text = $"Press 'F' to pick up {itemAsset.Name}";
	}

	public void Interaction ()
	{
		Item item = (Item)Activator.CreateInstance(itemClass);
		item.Apply(itemAsset);
		Equipment.Instance.AddItem(item);
		Destroy(this.gameObject);
	}

	protected void OnTriggerExit2D(Collider2D collision)
	{
		PopupText.Instance.MainGameObject.SetActive(false);
	}
}
