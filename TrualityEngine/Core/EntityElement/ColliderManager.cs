using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
namespace TrualityEngine.Core
{
    /// <summary>
    /// This is responsible for all collisions
    /// </summary>
    public sealed class ColliderManager
    {


        private readonly Dictionary<ColliderEntity, Stater> RemeberedCollision = new Dictionary<ColliderEntity, Stater>();
        private readonly SortedSet<MouseButton> RemeberedDraging = new SortedSet<MouseButton>();

        private bool OnOverInLastFrame;
        private bool IsEndedEndingOver;

        public Vect2 GridPos { get; private set; } = Vect2.Zero;    
                    
        public Vect2 Scale { get; set; } = new Vect2(1, 1);
        public ColliderEntity BaseEntity { get; private set; }
        /// <summary>
        /// If collider is not active then <see cref="IsPointIn(Vect2)"/> and <see cref="IsCollision(ColliderEntity)"/> always return false
        /// </summary>
        public bool IsActive { get; set; } = true;

        public ColliderManager Clone(ColliderEntity newEntity)
        {
            var collider = (base.MemberwiseClone() as ColliderManager);
            collider.BaseEntity = newEntity ?? throw new ArgumentNullException(nameof(newEntity));
            return collider;
        }


        public ColliderManager(ColliderEntity entity)
        {
            BaseEntity = entity;
            
          
        }



        public bool IsPointIn(Vect2 vector,PointFlags flags=PointFlags.None)
        {

            if (BaseEntity.ColliderManager.IsActive == false)
                return false;
            return BaseEntity.GetCollision().Contains(vector,flags);
        }

        public bool IsCollision(ColliderEntity entity, out Collider aInfo, out Collider bInfo)
        {
            if (this.IsActive == false || entity.ColliderManager.IsActive == false)
            {
                aInfo = null;
                bInfo = null;
                return false;
            }


            aInfo = BaseEntity.GetCollision();
            bInfo = entity.GetCollision();
            Collider aCopy;//I can use out in lambda :(
            aCopy = aInfo;
            return bInfo.IsTouch(aCopy);

        }
        public bool IsCollision(ColliderEntity entity) => IsCollision(entity, out _, out _);

        public IEnumerable<ColliderEntity> GetCollisionsWithAliveEntities()
        {
            return BaseEntity.GetCollision().GetCollisionsWithAliveEntities(BaseEntity);
        }
        public ColliderEntity GetCollisionWithAliveEntity()
        {
            return BaseEntity.GetCollision().GetCollisionWithAliveEntity(BaseEntity);
        }
        const float gridSize = 100f;
        private readonly List<ColliderEntity> registered = new List<ColliderEntity>();
         internal void RegisterCol(ColliderEntity entity)
        {
            registered.Add(entity);
        }
        internal void Update()
        {
            
            if (IsActive == false)
                return;
          
           

            foreach (ColliderEntity otherEntity in 
                ColliderEntity.GetOnlyColliders().Where(item => BaseEntity != item && item.ColiderGroup  == this.BaseEntity.ColiderGroup && item.ColliderManager.IsActive&&item.IsActive).ToArray())
            {

                bool result;
                if (registered.Any(item => item == otherEntity))
                    result = true;
                else 
                
                    result = IsCollision(otherEntity, out Collider a, out Collider b);

         

                bool contains;
                if ((contains = RemeberedCollision.ContainsKey(otherEntity)) == false && result == true)
                {
                    RemeberedCollision.Add(otherEntity, new Stater(State.FirstFrame));
                    contains = true;
                }
                  
                else if (contains)
                    RemeberedCollision[otherEntity]?.AddValue(result);


                if (contains)
                {
                    if (RemeberedCollision[otherEntity].State != State.None)
                        BaseEntity.CallByAll(i => i.IfCollision_(otherEntity, RemeberedCollision[otherEntity].State));
                    else
                        RemeberedCollision.Remove(otherEntity);
                }

             

            }
            
            registered.Clear();

            bool isMouseColliding;
            if (isMouseColliding = IsPointIn(Input.Actual.MousePosition,PointFlags.Mouse))
            {
                if (OnOverInLastFrame == false)
                    BaseEntity.CallByAll(i => i.IfMouseStartOver_());
                IsEndedEndingOver = false;
                OnOverInLastFrame = true;
                BaseEntity.CallByAll(i => i.IfMouseOver_());


            }

            else if (IsEndedEndingOver == false && OnOverInLastFrame == true)
            {
                BaseEntity.CallByAll(i => i.IfMouseEndOver_());
                IsEndedEndingOver = true;
                OnOverInLastFrame = false;
            }
            for (MouseButton button = 0; button < (MouseButton)3; button++)
            {
                if (Input.Actual.IsPressed(button))
                {
                    if (isMouseColliding)
                    {
                        if (Input.Actual.IsFirstFrame(button))
                            RemeberedDraging.Add(button);
                        BaseEntity.CallByAll(i => i.IfMousePressed_(button, Input.Actual.GetState(button)));
                    }

                }
                else
                    RemeberedDraging.Remove(button);

            }
            foreach (MouseButton draggingButton in RemeberedDraging)
                BaseEntity.CallByAll(i => i.IfMouseDrag_(draggingButton, Input.Actual.GetState(draggingButton)));


        }
       
    }
  

}
