using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace TrualityEngine.Core
{
	/// <summary>
	/// Base component version. You usually dont need this. Use <see cref="Component{T}"/>
	/// 
	/// </summary>
	public abstract class BaseComponent : EntityCaller
	{

		/// <summary>
		/// This can be added only <see cref="TrualityEngine.Core.Entity"/> which is it
		/// </summary>
		public Type Requier { get; }


		public string Name { get; set; }
		protected internal override void IfDraw(FixedBatch batch) { }
		public Entity Entity { get; private set; } = null;

		/// <summary>
		/// If <see cref="MultipleLock"/> equals <c>true</c> and you are adding two same objects, the game will throw an exception
		/// </summary>
		public bool MultipleLock { get; }
		public BaseComponent(bool multipleLock, Type requier)
		{
			MultipleLock = multipleLock;
			Requier = requier;
		}

		protected virtual void OnAdded()
		{

		}

		internal bool Connect(Entity entity)
		{
			if (entity.ComponentManager.GetAll().Any(item => item == this) == false)
				throw new Exception("Connection failed");
			bool result = entity == null;
			if (Entity == null)
			{
				Entity = entity;
				OnAdded();
			}

			return result;


		}

		public override Entity GetFrom() => this.Entity;
		protected override IEnumerable<EntityCaller> GetToTestWhenActiveChaned()
		{
			return new EntityCaller[] { };
		}
		public override bool IsActive
		{
			get => IsActiveSelf && (Entity?.IsActive ?? false);
		}

		public override void Update()
		{
			base.Update();
			IfUpdate();
		}

		public override void Kill()
		{
			base.Kill();
			if (IsKill == false)
			{
				this.Entity.ComponentManager.Remove(this);


			}

		}



	}
}
