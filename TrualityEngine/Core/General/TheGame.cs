using TrualityEngine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TrualityEngine
{

	public sealed class TheGame : Game
	{
		public GraphicsDeviceManager Graphics { get; set; }
		private FixedBatch FixedBatch { get; set; }

		public bool IsIniting { get; private set; }
		public static TheGame Instance { get; private set; }
		public GameHeart MainGame { get; }


		public long Frames { get; private set; }


        public TheGame(GameHeart mainGame)
		{
			Instance = this;
			MainGame = mainGame ?? throw new ArgumentNullException(nameof(mainGame));
			MainGame.BaseGame = this;
            GameHeart.Actual = MainGame;
			Graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
            this.Window.AllowUserResizing = true;
            
            IsFixedTimeStep = false;





		}


		protected override void Initialize()
		{
			

			IsIniting = true;
          
            MainGame.Initialization();
            
			IsIniting = false;
            this.Window.Title = MainGame.GameName;
            base.Initialize();
			
		}


		protected override void LoadContent()
		{
			this.IsMouseVisible = true;
			
			FixedBatch = new FixedBatch(GraphicsDevice);
			this.Exiting += (s, e) => MainGame.OnClose();
            
			LoadContext();
			MainGame.LoadContent();
			SynchronizationContext.SetSynchronizationContext(new TrualityContext());

			MainGame.Start();

		}
		public static void LoadContext()
		{

			// Don't remove this method // K.
		}


		protected override void UnloadContent()
		{
			MainGame.Unload();
		}


		protected override void Update(GameTime gameTime)
		{

			MainGame.Update(gameTime);
			GameManager.Update(gameTime.ElapsedGameTime);
			base.Update(gameTime);
		}




		protected override void Draw(GameTime gameTime)
		{
			if (Camera.Main == null)
				return;
			GraphicsDevice.Clear(Camera.Main.Background);


			if (Camera.Main != null)
			{
				FixedBatch.Begin(Camera.Main.GetMatrix());
				GameManager.Batch(FixedBatch);
				FixedBatch.End();
			}
			base.Draw(gameTime);
		}
	}
}
