using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrualityEngine.Core;
using Microsoft.Xna.Framework.Input;
namespace GameJamProject
{
	public class MyCustomEntity : SpriteEntity//or LineEntity or TextEntity or..
	{
		protected override void IfUpdate()
		{
			base.IfUpdate();
			//update code

		}

		protected override void IfStart()
		{
			base.IfStart();
			//start code
		}
	}
}
