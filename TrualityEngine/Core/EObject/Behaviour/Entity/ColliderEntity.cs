using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
namespace TrualityEngine.Core
{
	public enum BodyType
	{

		/// <summary>
		/// Strong can move it and it cannot moving anything
		/// </summary>
		Base = 0,
		/// <summary>
		/// Static cannot be moved
		/// </summary>
		Static = 1,
		/// <summary>
		/// Strong moving no strong object and ignore other strong
		/// </summary>
		Strong = 2,
		/// <summary>
		/// Trigger ignore phycisc
		/// </summary>
		Trigger = 3,
	}



	/// <summary>
	/// Entity that can read and emmiter collision
	/// </summary>
	public class ColliderEntity : Entity
	{

        
        public PhysicsManager Physics { get; private set; }
      
		public ColliderManager ColliderManager { get; private set; }
        private static readonly List<ColliderEntity> allCollidersEntites = new List<ColliderEntity>();
        internal static  ReadOnlyCollection<ColliderEntity> GetOnlyColliders() => allCollidersEntites.AsReadOnly();
		public uint ColiderGroup { get; set; }
		public ColliderLayer ColliderLayer { get; set; } = ColliderLayer.Base;

		public virtual Collider GetCollision()
		{
			Vect2 fullPos = GetPosWithAllOffsets();
			Vect2 totalScale = GetTotalColliderScale();
            return new Collider(new Rect((int)fullPos.X - ((int)totalScale.X) / 2, (int)fullPos.Y - (int)totalScale.Y / 2, (int)totalScale.X, (int)totalScale.Y));

                
		}

        public bool IsOnWall(Vect2 wallDirection)
        {
            if (wallDirection.X > 1)
                wallDirection = new Vect2(1, wallDirection.Y);

            if (wallDirection.X < -1)
                wallDirection = new Vect2(-1, wallDirection.Y);

            if (wallDirection.Y > 1)
                wallDirection = new Vect2(wallDirection.X, 1);

            if (wallDirection.Y < -1)
                wallDirection = new Vect2(wallDirection.X, -1);

            return Physics.TestMove(wallDirection);
        }

        public bool IsOnFloor(Vect2 upVector)
        {
            if (upVector.X > 1)
                upVector = new Vect2(1, upVector.Y);

            if (upVector.X < -1)
                upVector = new Vect2(-1, upVector.Y);

            if (upVector.Y > 1)
                upVector = new Vect2(upVector.X, 1);

            if (upVector.Y < -1)
                upVector = new Vect2(upVector.X, -1);


            return IsOnWall(-upVector);
        }


        protected override void IfStart()
		{
			base.IfStart();

		}

		public override void Update()
		{
			base.Update();


		}
		protected override void IfFixedUpdate()
		{
			base.IfFixedUpdate();
			Physics.FixedUpdate();
		}


		public ColliderEntity()
		{
			ColliderManager = new ColliderManager(this);
			Physics = new PhysicsManager(this);
            allCollidersEntites.Add(this);
		}
        protected override void IfKill()
        {
            base.IfKill();
            allCollidersEntites.Remove(this);
        }
        protected virtual Vect2 GetTotalColliderScale() => FullScale * ColliderManager.Scale;
		protected override void IfUpdate()
		{
			base.IfUpdate();

			ColliderManager.Update();

		}
	}
}
