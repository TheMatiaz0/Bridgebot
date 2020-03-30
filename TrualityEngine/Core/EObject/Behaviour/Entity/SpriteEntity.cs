 

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrualityEngine.Core
{
	/// <summary>
	/// If it is alive it will render 2D sprite.
	/// </summary>
	public class SpriteEntity : RenderEntity
	{
        private static Sprite BaseS { get; } = SpriteCreator.MakeRectangle(new Point(1, 1), Color.White);
		protected override Color GetRenderColor()
		{

			Color bColor = base.GetRenderColor();
			AnimFrame frame = AnimationManager.Current?.GetAnimFrame();
			if (frame == null)
				return bColor;
			Color color = bColor;
			if (frame.Color != null)
				color = Color.Lerp(bColor, (Color)frame.Color, frame.Ammount);
			if (frame.Alpha != null)
				return new Color(color, (float)AnimationManager.Current.GetAnimFrame().Alpha);
			else return color;
		}
		protected override Rotation GetSpecialRotate()
		{
			return base.GetSpecialRotate() + (AnimationManager.Current?.GetAnimFrame().Rotate ?? Rotation.Zero);
		}
		public AnimationManager AnimationManager { get; private set; }
		private Sprite _Sprite;

		public Sprite Sprite
		{
			get => _Sprite;
			set
			{


				_Sprite = value;
				OnSpriteChanged.Invoke(this, value);

			}
		}
		/// <summary>
		/// Scaling RenderScale to to texture size
		/// </summary>
		public void ScaleRenderScaleToSpriteTexture()
		{
			PrivateScale = new Vect2(Sprite.Value.Width, Sprite.Value.Height);
		}
		public Event<SimpleArgs<Sprite>> OnSpriteChanged { get; protected set; } = Event.Empty;

		protected override Vect2 GetTotalColliderScale()
		{
			return base.GetTotalColliderScale();
		}
		protected internal override void IfDraw(FixedBatch batch)
		{

			base.IfDraw(batch);
			if (Sprite != null && Sprite.Value != null)
				batch.Draw(Sprite.Value, Pos + RenderOffset, FullScale, GetRenderColor(), FullRotate, SpriteEffects.None);


		}
		public SpriteEntity(Sprite sprite = null, Vect2? renderScale = null)
		{
			Sprite = sprite??BaseS;
			if (renderScale != null)
				this.PrivateScale = (Vect2)renderScale;
			this.RenderColor = Color.White;
			AnimationManager = new AnimationManager(this);
		}
		public SpriteEntity() : this(null)
		{

		}
		public SpriteEntity(bool scaleToTexture, Sprite sprite) : this
			(sprite, null)
		{
			if (scaleToTexture)
				this.ScaleRenderScaleToSpriteTexture();
		}
		protected override Vect2 GetSpecialOffset()
		{
			return base.GetSpecialOffset() + AnimationManager.Current?.GetAnimFrame().Offset ?? Vect2.Zero;
		}
		protected override Vect2 GetSpecialScale()
		{
			return base.GetSpecialScale() * (AnimationManager.Current?.GetAnimFrame().Scale ?? new Vect2(1, 1));

		}

		protected override void IfUpdate()
		{
			base.IfUpdate();
			AnimationManager.Update();

		}






	}
}
