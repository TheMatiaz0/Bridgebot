using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cyberevolver.Unity
{
	/// <summary>
	/// Hides UI camera's culling layer after clicking specific key on keyboard.
	/// </summary>
	public class UIHider : AutoInstanceBehaviour<UIHider>
	{
		[SerializeField] private KeyCode keyCodeActive = KeyCode.F1;

		[SerializeField]
		private CanvasGroup uiElements = null; 
		public bool IsHidden { get; private set; } = false;

		protected void Update()
		{
			CullingMaskChange();
		}

		private void CullingMaskChange()
		{
			if (Input.GetKeyDown(keyCodeActive))
			{
				Switch();
			}
		}

		public void Switch ()
		{
			IsHidden = !IsHidden;
			HideUI(IsHidden);
		}

		public void HideUI (bool isTrue)
		{
			if (isTrue)
			{
				uiElements.alpha = 0f;
			}

			else
			{
				uiElements.alpha = 1f;
			}
		}
	}
}

