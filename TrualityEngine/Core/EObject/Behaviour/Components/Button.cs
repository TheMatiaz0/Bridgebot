 

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
	/// <summary>
	/// Button component. If you add this, you do should not changing <see cref="RenderEntity.RenderColor"/>. 
	/// If you want change base color use <see cref="Button.BaseColor"/>
	/// </summary>
	public class Button : Component<RenderEntity>
	{
		protected Coroutine PressingTask = null;
		private bool AfterClickLock = false;


		/// <summary>
		/// Final color is lerp this and actual effect color
		/// </summary>
		public Color? BaseColor { get; set; }
		/// <summary>
		/// If you create <see cref="Button"/> this from constructor,it will be null
		/// </summary>

		public TextEntity TextEntity { get; set; }
		/// <summary>
		/// If it's true, user will be able to press the button
		/// </summary>
		public bool IsInteractable
		{
			get => _Interactable;
			set
			{
				_Interactable = value;
				OnInteractableChange.Invoke(this, value);
			}
		}

		private bool _Interactable;

		/// <summary>
		/// Color that occurs when the user is pressing
		/// </summary>
		public Color PressedColor { get; set; }
		/// <summary>
		/// Color that occurs when the cursor is over entity
		/// </summary>
		public Color HighlightedColor { get; set; }
		/// <summary>
		/// Color that occurs when IsInteractable equals false
		/// </summary>
		public Color DisableColor { get; set; }
		/// <summary>
		/// Color that occurs when IsInteractable equals true and move does not over this entity and user does not press
		/// </summary>
		public Color EnableColor { get; set; }
		private Color GetTrueColor(Color affectColor) => Color.Lerp(affectColor, BaseColor ?? affectColor, 0.7f);

		public Event<SimpleArgs<Button>> OnClick { get; protected set; } = Event.Empty;
		public Event<SimpleArgs<bool>> OnInteractableChange { get; protected set; } = Event.Empty;

		protected override void IfUpdate()
		{
			base.IfUpdate();
			if (AfterClickLock && Input.Actual.IsEndOfPress(MouseButton.Left))
				AfterClickLock = false;
		}

		protected override void OnAdded()
		{
			base.OnAdded();
			BaseColor = Entity.RenderColor;
		}

		protected override void IfStart()
		{

			OnInteractableChange.Value += IfInteractableChange;
			PressedColor = new Color(107, 107, 107);
			HighlightedColor = new Color(100, 100, 100,200);
			EnableColor = Color.White;
			IsInteractable = true;
			DisableColor = new Color(100, 100, 100);
			Entity.RenderColor = GetTrueColor(EnableColor);
		}
		private IEnumerator<ICoroutineable> Pressing()
		{
			AfterClickLock = true;
			OnClick.Invoke(this, this);
			Entity.RenderColor = GetTrueColor(PressedColor);
			yield return Async.WaitTime(0.1f);

			if (Entity.ColliderManager.IsPointIn(Input.Actual.MousePosition))
			{
				Entity.RenderColor = GetTrueColor(HighlightedColor);
			}
			else
			{
				Entity.RenderColor = GetTrueColor(((IsInteractable) ? EnableColor : DisableColor));
			}



		}
		private void SetInteractableColor(Color color, bool ignore = false)
		{
			if (ignore || this.IsInteractable && (PressingTask == null || PressingTask.IsEnded))
			{
				Entity.RenderColor = GetTrueColor(color);
			}

		}

		private void IfInteractableChange(object sender, SimpleArgs<bool> value)
		{
			if (value == false)
				Entity.RenderColor = DisableColor;
			else
				Entity.RenderColor = EnableColor;
		}



		protected override void IfMouseStartOver()
		{
			base.IfMouseStartOver();
			SetInteractableColor(HighlightedColor);
		}
		protected override void IfMouseEndOver()
		{
			base.IfMouseEndOver();
			SetInteractableColor(EnableColor);
		}
		protected override void IfMouseSingleClick(MouseButton mouseButton)
		{
			base.IfMouseSingleClick(mouseButton);
			if ((AfterClickLock == false && mouseButton == MouseButton.Left))
				if (IsInteractable == true && (PressingTask == null || PressingTask.IsEnded))
					PressingTask = Yield.Start(Pressing());
		}



	}
}
