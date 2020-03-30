using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
	/// <summary>
	/// Manager of main game logic
	/// </summary>
	public static class GameManager
	{
		public static void Update(TimeSpan delta)
		{


			TimeOfGame.Actual.NextFrame(delta);
			(SynchronizationContext.Current as TrualityContext)?.UpdateFrame();
            Input.Actual.InputUpdate();


			if (TimeOfGame.Actual.GetFullTimeScale() > 0)
				SceneRuntime.ActualScene.Update();

            Entity.MakeEntityFree();//removing all entity which made to kill themself.


		}
		internal static void Batch(FixedBatch fixedBatch)
		{

			SceneRuntime.ActualScene.Draw(fixedBatch);
		}


	}
}
