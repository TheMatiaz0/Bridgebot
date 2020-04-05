using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpManager : MonoBehaviour
{
	[SerializeField]
	private GameObject hpSingleObject = null;

	public GameObject HpSingleObject => hpSingleObject;

	[SerializeField]
	private Transform parentForHp = null;

	public Transform ParentForHp => parentForHp;

	private Hp _CurHealth;

	[SerializeField] private Color lifeColor = Color.green;
	[SerializeField] private Color deadColor = Color.red;

	public Hp CurHealth
	{
		get => _CurHealth;
		set
		{
			if (_CurHealth != null)
				_CurHealth.OnValueChanged -= OnCurHealthChanged;
			_CurHealth = value;
			_CurHealth.OnValueChanged += OnCurHealthChanged;
		}
	}

	protected void Start()
	{
		Refresh();
	}

	private void OnCurHealthChanged(object sender, Hp.HpChangedArgs args)
	{
		Refresh();
	}

	public void Refresh()
	{
		if (CurHealth == null)
			return;

		int index = 0;
		foreach (Transform item in parentForHp)
		{
			index++;
			item.GetComponent<Image>().color = (index > CurHealth.Value) ? deadColor : lifeColor;

		}
	}
}
