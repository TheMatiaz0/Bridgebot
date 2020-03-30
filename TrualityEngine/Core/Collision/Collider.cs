using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    
    /// <summary>
    /// Represents colliden from one frame. Can be getting from <see cref="ColliderEntity"/>.
    /// </summary>
	public class Collider
	{
		public ICollisionInfo[] CollisionInfos { get; }
        /// <summary>
        /// Collider group is one from two options to lock collision with not important object
        /// If two object have other <see cref="ColliderGroup"/> won't be colliding.
        /// </summary>
        public uint? ColliderGroup { get;  }
        /// <summary>
        /// ColliderLayer  is one from two options to lock collision with not important object.
        /// It accept only concret <see cref="ColliderLayer"/> as Collider.It should be set on Init in <see cref="GameHeart"/>
        /// </summary>
        public ColliderLayer ColliderLayer { get; }
        public Collider(uint? coliderGroup, ColliderLayer colliderLayer, IEnumerable<ICollisionInfo> collection)
        {
            CollisionInfos = collection?.ToArray() ?? throw new ArgumentNullException(nameof(collection));
            ColliderGroup = coliderGroup;
            ColliderLayer = colliderLayer ?? ColliderLayer.Base;


        }
        public Collider(params ICollisionInfo[] collisionInfo) 
            : this(null,null,collection: collisionInfo)
		{

		}
        public static Collider Create<T>(params T[] infos)
            where T : ICollisionInfo
            => Create<T>(null, null, infos);
        public static Collider Create<T>(IEnumerable<T> infos)
            where T : ICollisionInfo
            => Create(null, null, infos);
        public static Collider Create<T>(uint? colliderGroup, ColliderLayer colliderLayer, IEnumerable<T> infos)
            where T : ICollisionInfo
            => new Collider(colliderGroup, colliderLayer, infos.Cast<ICollisionInfo>());
        internal IEnumerable<ColliderEntity> GetCollisionsWithAliveEntities(ColliderEntity except=null)
        {
            return (from c in ColliderEntity.GetOnlyColliders()
                    where c != except && c.ColliderManager.IsActive && c.IsActive
                    && c.GetCollision().IsTouch(this)
                    select c);
        }
        internal ColliderEntity  GetCollisionWithAliveEntity(ColliderEntity except=null)
        {
            ColliderEntity[] colliders = GetCollisionsWithAliveEntities(except).ToArray();
            if (colliders.Length > 0)
                return colliders[0];
            else
                return null;
        }
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public Collider(ColliderEntity coliderEntity, params ICollisionInfo[] collisionInfo):this(coliderEntity.ColiderGroup,coliderEntity.ColliderLayer,collisionInfo)
        { }
        public Collider(ColliderEntity coliderEntity, IEnumerable<ICollisionInfo> collisionInfo) : this(coliderEntity.ColiderGroup, coliderEntity.ColliderLayer, collisionInfo)
        { }
        /// <summary>
        /// Checking if other colliding with this.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if this collider and other collide with each other. <see cref="ColliderGroup"/> and <see cref="ColliderLayer"/> can affect to result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool IsTouch(Collider other)
		{
            if (other == null)
                throw new ArgumentNullException(nameof(other));
            if ((this.ColliderGroup == null || other.ColliderGroup == null || (other.ColliderGroup == this.ColliderGroup))
                && (this.ColliderLayer.IsGood(other.ColliderLayer)))
                return other.CollisionInfos.Any(a => this.CollisionInfos.Any(b => b.Intersects(a)));
            else
                return false;
		}
        /// <summary>
        /// Checking if this colliding with the concret point.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="flags">if this collider ignore minimum one flag from it, method will definitely return false</param>
        /// <returns>true if pos colliding with this and anything point flags isn't in ignore</returns>
		public bool Contains(Vect2 pos, PointFlags flags = PointFlags.None)
		{
            if (ColliderLayer.IsGood(flags))
                return this.CollisionInfos.Any(item => item.Contains(pos));
            else
                return false;
		}
	}
}
