 
using System;
using System.Collections.Generic;
using System.Linq;
namespace TrualityEngine.Core
{
	/// <summary>
	/// Runtime scene will be created by BlazeEngine and will be destroyed by BlazeEngine
	/// </summary>
	public sealed class SceneRuntime : Behaviour
	{
		private float _TimeScale = 1f;

		public static SceneRuntime ActualScene { get; private set; }
		public SceneBehaviour ScenePrefix { get; }



		/// <summary>
		/// Final TimeScale is equal ActualSceneTimeScale*GlobalTimeScale
		/// If you want set global timeScale, then you should use Time.GlobalTimeScale
		/// </summary>
		public float TimeScale
		{
			get => _TimeScale;
			set
			{
				if (value < 0)
					throw new ArgumentException($"{nameof(value) }cannot be lesser then 0", nameof(value));
				_TimeScale = value;
			}
		}

		public override bool IsActive
		{
			get => true;

		}
		static SceneRuntime()
		{

			//  ActualScene = new SceneRuntime(new ScenePrefix());
		}
		private SceneRuntime(SceneBehaviour prefix)
		{
			ScenePrefix = prefix;

			IsKill = false;
			if (prefix != null)
				prefix.Scene = this;


		}



		public static SceneRuntime ChangeScene(SceneBehaviour prefix)
		{

			ActualScene?.ScenePrefix?.BeforeClosingScene();

            Entity.BeginLockSpawn();
			foreach (Entity entity in Entity.GetEntities().ToArray())
				if (entity.IsGlobal == false && entity.Parent == null && entity.IsKill == false)
					entity.Kill();
            Entity.EndLockSpawn();


			ActualScene?.ScenePrefix?.AfterClosingScene();
			if (ActualScene != null)
				ActualScene.Kill();
			if (Camera.Main != null && Camera.Main.IsKill == false)
				Camera.Main.Kill();
			if (Listener.Main != null && Listener.Main.IsKill == false)
				Listener.Main.Kill();
			new Camera().SetToMain().AddChild(new Listener().SetToMain());

			if (ActualScene != null)
			{
				ActualScene.IsKill = true;
				if (ActualScene.ScenePrefix != null)
					ActualScene.ScenePrefix.Scene = null;

			}

			ActualScene = new SceneRuntime(prefix);

			if (ActualScene.ScenePrefix != null)
				foreach (Action action in ActualScene.ScenePrefix.GetCreator())
					action();
			ActualScene.ScenePrefix?.SceneLoaded();
            Entity.MakeEntityFree();
			return ActualScene;

		}
		public override void Update()
		{
			base.Update();

			ScenePrefix?.Update();
			Entity.Call(Entity.GetEntities(), entity => entity.Update());
			ScenePrefix?.LateUpdate();


		}
		public void Draw(FixedBatch fixedBatch)
		{
			ScenePrefix?.Draw(fixedBatch);

			foreach (RenderEntity entity in from entity in Entity.GetEntities()
											where entity.IsActive && entity is RenderEntity
											orderby entity ascending
											select entity as RenderEntity)
			{
				bool wasRender;
				if ((wasRender = Camera.Main.WillBeRender(entity.GetDrawCollisonInfo())))
					entity.Draw(fixedBatch);

				if (entity.WasRenderInLastFrame != wasRender)
					entity.CallByAll(item => item.IfEntityWillRenderIsChanged_(!entity.WasRenderInLastFrame));


				entity.WasRenderInLastFrame = wasRender;
			}
		}



		public override void Kill()
		{
			base.Kill();

		}
	}
}
