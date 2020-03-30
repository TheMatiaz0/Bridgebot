 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TrualityEngine.Core
{
   
   

    /// <summary>
    /// Represents base object in the scene, it cannot read or emit collision or draw anything
    /// </summary>
    public class Entity : EntityCaller
    {
        private static List<Entity> Entities { get; } = new List<Entity>();
        private static HashSet<Entity> ToKill { get; } = new HashSet<Entity>();

        private static bool CannotSpawn { get;  set; } = false;

      
        internal bool WasRenderInLastFrame { get; set; }
        private List<SoundEffectInstance> DynamicSounds { get; set; }

        /// <summary>
        /// Get all non-killed entities
        /// </summary>
        /// <returns></returns>
        public static IReadOnlyList<Entity> GetEntities() => Entities;
       
        public Event<ValueInEntityChangedArgs<Vect2>> OnScaleChanged { get; protected set; } = Event.Empty;

        private List<Entity> Children { get; set; } = new List<Entity>();
        
        public ComponentManager ComponentManager { get; private set; }
        public Entity Parent { get; private set; }
        public T[] GetChilds<T>() => Children.Where(item => item is T).Cast<T>().ToArray();
        public IReadOnlyList<Entity> GetChilds() => Children;

        /// <summary>
        /// Rotation affects children
        /// Warning! Collider will not rotate
        /// </summary>
      
        public Rotation SelfRotate { get; set; }

        /// <summary>
        /// If it's true the entity will not be destroyed, if the scene is loading
        /// </summary>
        public bool IsGlobal { get; set; }
       /// <summary>
       /// Entity name.It is using during <see cref="ToString"/>, and also can be used to find a entity.
       /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Entity's tags.
        /// </summary>
        public Tags Tags { get; set; } 
       
        public Vect2 OwnPos { get; set; }
        /// <summary>
        /// Finally scale will be use to draw.
        /// </summary>
        public Vect2 FullScale
        {
            get
            {
                

                Entity entity = this;
                Vect2 totalScale = new Vect2(1, 1);
                do
                {
                    totalScale *= entity.Scale;
                    entity = entity.Parent;
                }
                while (entity != null);
                return totalScale * GetSpecialScale();

            }
        }
        /// <summary>
        /// Position in the space.
        /// </summary>
        [IniProp]
        public Vect2 Pos
        {
            get
            {
                
                if (Parent == null)
                    return OwnPos;
                else
                    return (OwnPos.Rotate(-(Parent.FullRotate.Angle-((Parent as RenderEntity)?.RenderRotation.Angle??0))) + Parent.Pos);
            }
            set
            {
                if(value!=Pos)
                {
                    if (Parent == null)
                        OwnPos = value;
                    else
                        OwnPos = value - Parent.Pos;
                    
                }
               
            }
        }
        private Vect2 _Scale;
        /// <summary>
        /// Scale that affects on child
        /// </summary>
      
        public Vect2 Scale
        {
            get => _Scale;
            set
            {
                if (value != _Scale)
                {

                    _Scale = value;
                    OnScaleChanged.Invoke(this, new ValueInEntityChangedArgs<Vect2>(this, value));
                    
                }

            }
        }
        /// <summary>
        /// Return IsActive in hierarchy.
        /// If you want to set self-active, you should use ActiveSelf property
        /// </summary>
       
        public override bool IsActive
        {
            get
            {
                Entity entity = this;

                bool result;
                do
                {
                    result = entity.IsActiveSelf;
                    entity = entity.Parent;

                } while (entity != null && result == true);
                return result;
            }

        }
        /// <summary>
        /// Includes Rotate, parent's rotation and RenderRotate
        /// </summary>
        
        public Rotation FullRotate
        {
            get => GetSpecialRotate() + SelfRotate + (Parent?.SelfRotate ?? Rotation.Zero);

        }
        public Entity(string name = "unnamed", Vect2? pos = null, Vect2? scale = null, Tags tags = new Tags(), Rotation? rotation = null)
        {
            if (CannotSpawn)
                throw new TrualityEngine.Core.WrongCreatingContextException();

            Name = name;
            Tags = tags;
            SelfRotate = rotation ?? Rotation.Zero;
            Pos = pos ?? Vect2.Zero;
            Scale = scale ?? new Vect2(1, 1);
            ComponentManager = new ComponentManager(this);

            Children = new List<Entity>();
            DynamicSounds = new List<SoundEffectInstance>();
            Entities.Add(this);

        }
        internal static void BeginLockSpawn()
        {
            if (CannotSpawn == false)
                CannotSpawn = true;
            else
                throw new InvalidOperationException("That have begun earlier");
        }
        internal static void EndLockSpawn()
        {
            if (CannotSpawn)
                CannotSpawn = false;
            else
                throw new InvalidOperationException("The Stack is empty");
        }

        public Entity[] GetTotalChild()
        {
            if (Children.Count == 0)
                return new Entity[0];
            List<Entity> total = new List<Entity>();
            foreach (var child in Children)
            {
                total.Add(child);
                total = child.GetTotalChild().Union(total).ToList();
            }
            return total.ToArray();

        }
        public void LookAt(Vect2 target)
        {

            this.SelfRotate = CalculateLookAt(target);
        }
        public Rotation CalculateLookAt(Vect2 target)
        {
            Vect2 dis = FixedBatch.ToPrimitivePosition(target) - FixedBatch.ToPrimitivePosition(this.Pos);
            float radians = (float)Math.Atan2(dis.Y, dis.X);
            return Rotation.FromRadians(radians);
        }
       
        public Entity():this(pos:null)
        {

        }
        public override void Update()
        {

            base.Update();
           

            foreach (SoundEffectInstance instance in DynamicSounds)
            {
                if (Listener.Main != null)
                {
                    if (instance.State == SoundState.Paused)
                        instance.Play();
                    float distance = Vect2.Distance(Listener.Main.Pos, Pos) + 1;
                    var val = 1 / distance;
                    instance.Volume = val;
                }
                else
                    instance.Pause();

            }


            Behaviour.Call(ComponentManager.GetAll(), item => item.Update());

            IfUpdate();
        }
        public override Entity GetFrom() => this;
        protected sealed override IEnumerable<EntityCaller> GetToTestWhenActiveChaned()
        {

            List<EntityCaller> callers = ComponentManager.GetAll().Cast<EntityCaller>()
                .Union(Children.Cast<EntityCaller>()).ToList();
            foreach (Entity entity in Children)
                callers = callers.Union(entity.GetToTestWhenActiveChaned()).ToList();
            return callers;
        }


        public sealed override void Kill()
        {

            base.Kill();
            ToKill.Add(this);
            Behaviour.Call(this.Children, item => item.Kill());
            CallByAll(item => item.Kill(), false);
            
            IsKill = true;

        }
        public override int GetHashCode()
        {
            return Id;
        }
        internal static void MakeEntityFree()
        {
            foreach(Entity entity in ToKill)
            {
                if (entity.Parent != null)
                {
                    entity.Parent.RemoveChild(entity);
                    entity.Parent = null;
                }
                Entities.Remove(entity);
            }
            ToKill.Clear();
        }
        public override string ToString()
        {
            return Name;
        }




        internal void CallByAll(Action<EntityCaller> caller, bool includeThis = true)
        {
            if(ComponentManager.IsActive)
                foreach (BaseComponent component in this.ComponentManager.GetAll())
                    caller(component);

            if (includeThis)
                caller(this);
        }

       

        public T As<T>()
            where T : Entity
            => this as T;

        public Vect2 GetPosWithAllOffsets()
            => Pos + GetSpecialOffset();

        public static T Create<T>(string name = "unnamed", Vect2? pos = null, Vect2? scale = null, Tags tags = new Tags(), Rotation rotation = new Rotation())
          where T : Entity, new()
        {
            return new T() { Name = name, Pos = pos ?? new Vect2(0, 0), Scale = scale ?? new Vect2(1, 1), Tags = tags, SelfRotate = rotation };
        }



        /// <summary>
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entity">Entity cannot be killed or null or already connected with this object</param>
        /// <returns></returns>
        public T AddChild<T>(T entity)
            where T : Entity
        {
            if (entity == this)
                throw new Exception("You cannot add the same object");
            if (entity == null)
                throw new NullReferenceException("Entity cannot be a null");
            if (entity.IsKill)
                throw new ObjectIsKilledException(entity);
            Children.Add(entity);
            if (entity.Parent != null)
                entity.Parent.RemoveChild(entity);
            entity.Parent = this;
            entity.OwnPos -= this.Pos;
            return entity;
        }
        public void SetParent(Entity parent)
        {
            parent.AddChild(this);
        }

        public bool RemoveChild(Entity entity)
        {
            if (Children.Any(item => item == entity) == false)
                return false;
            entity.OwnPos = Pos;
            entity.Parent = null;
            this.Children.Remove(entity);
            return true;

        }
        #region Music

        /// <summary>
        /// Dynamic play volume depends on main listener position
        /// </summary>
        /// <param name="soundEffect"></param>
        public void DynamicPlay(SoundEffect soundEffect, float volume = 0f, Action<SoundEffectInstance> settings = null)
        {

            SoundEffectInstance instance = soundEffect.CreateInstance();
            instance.Volume = volume;
            settings?.Invoke(instance);
            instance.Play();
            DynamicSounds.Add(instance);
            base.SoundsInstances.Add(instance);

        }
        /// <summary>
        /// Dynamic play volume depends on main listener position
        /// </summary>
        /// <param name="soundEffect"></param>
        public void DynamicPlay(string soundEffect, float volume = 0f, Action<SoundEffectInstance> settings = null)
        {
            DynamicPlay(Asset<SoundEffect>.Get(soundEffect).Value, volume, settings);
        }

        #endregion

        protected virtual Rotation GetSpecialRotate() => Rotation.Zero;
        protected virtual Vect2 GetSpecialScale() => new Vect2(1, 1);
        protected virtual Vect2 GetSpecialOffset() => new Vect2(0, 0);

        #region static
        /// <summary>
        /// Find all Entities that return true by predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Entity[] FindAll(Predicate<Entity> predicate)
            => Entities.FindAll(predicate).ToArray();
        /// <summary>
        /// Find all Entities that have the same name as in the parameter.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Entity[] FindAllByName(string name)
            => Entities.FindAll(item => item.Name == name).ToArray();
        /// <summary>
        /// Find all Entities by generic type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] FindAllByType<T>()
            where T : Entity
            => (FindAll(item => item is T).Cast<T>().ToArray());
        /// <summary>
        /// Find all Entities that have this flag and they don't have any other flags.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static Entity[] FindAllIdenticTags(Tags tag)
            => FindAll(item => item.Tags == tag).ToArray();
        /// <summary>
        /// Find all Entities that have this flag.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static Entity[] FindAllHasTags(Tags tag)
           => FindAll(item => item.Tags.Has(tag)).ToArray();

        /// <summary>
        /// Find Entity that returns true by predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Entity Find(Predicate<Entity> predicate)
            => Entities.Find(predicate);
        /// <summary>
        /// Find Entity that has the same name as in the parameter.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Entity FindByName(string name)
            => Find(item => item.Name == name);
        /// <summary>
        /// Find Entity by generic type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FindByType<T>()
            where T : Entity
            => Find(item => item is T) as T;
        /// <summary>
        /// Find Entity that has flag in the parameter and it doesn't have any other flags. 
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static Entity FindIdenticalTags(Tags tag)
            => Find(item => item.Tags == tag);
        /// <summary>
        /// Find Entity that has flag in the parameter.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static Entity FindEntityThatHasTags(Tags tag)
            => Find(item => item.Tags.Has(tag));

      
        
       
        #endregion




    }
}
