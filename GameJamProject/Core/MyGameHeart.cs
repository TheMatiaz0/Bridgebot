using TrualityEngine;
using Microsoft.Xna.Framework;
using TrualityEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJamProject
{
	public class MyGameHeart : GameHeart
	{
		public override void Initialization()
		{
			base.Initialization();
			//put here init logic
			base.GameName = "GameJamProject";
			//put here collider layer creating code
			var c = ColliderLayer.Create("IgnoreAll");
			c.RemoveAllLayerAccept();
			c.DontAcceptFlags = PointFlags.Mouse;
			c.AcceptTheSameLayer = false;


		}

		public override void Start()
		{
			base.Start();
			SceneRuntime.ChangeScene(new FirstScene());  // Changes scene to FirstScene
		}
	}
}
