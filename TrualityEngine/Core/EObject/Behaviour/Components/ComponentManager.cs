
using System;
using System.Collections.Generic;
using System.Linq;
namespace TrualityEngine.Core
{
    public class ComponentManager : CollectionManager<BaseComponent>, IEntityManager<Entity>
    {


        /// <summary>
        /// Entity, that is connected with the ComponentManager
        /// </summary>
        public Entity Entity { get; }
        public bool IsActive { get; set; } = true;

        public Event<AddedComponentArgs> OnAddComponent { get; protected set; } = Event.Empty;

        public ComponentManager(Entity entity) : base(false)
        {
            Entity = entity;
            base.OnAddElement.Value += ComponentManager_OnAddElement;
            base.OnRemoveElement.Value += ComponentManager_OnRemoveElement;
        }

        private void ComponentManager_OnRemoveElement(object sender, CollectionManagerArgs<BaseComponent> e)
        {
            if (e.Element.IsKill == false)
                e.Element.Kill();
        }

        private void ComponentManager_OnAddElement(object sender, CollectionManagerArgs<BaseComponent> e)
        {
            e.Element.Connect(Entity);

            OnAddComponent.Invoke(this, new AddedComponentArgs(e.Element, Entity));
        }

        /// <summary>
        /// Adding new <see cref="Component{TEntityRequirer}"/>. 
        ///  <see cref="Entity"/> must fit to <see cref="BaseComponent.Requier"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Add<T>()
            where T : BaseComponent, new()
            => Add(new T()) as T;

        public T Get<T>()
            where T:class
            => List.Find(item => item is T) as T;
        public T[] GetAll<T>()
            where T : class
            => List.FindAll(item => item is T).Cast<T>().ToArray();

        protected override bool CanAdd(BaseComponent component)
        {


            if (TheReflection.Is(Entity.GetType(), component.Requier)==false)
                throw new ArgumentException($"Component requiers {{{component.Requier}}} type or his inheritance");
            if (component == null)
                throw new ArgumentNullException(nameof(component), "Argument cannot be null");
            if (component.Entity != null)
                throw new ArgumentException("Component is already connected", nameof(component));
            if (component.IsKill)
                throw new ArgumentException("Component is already killed ", nameof(component));
            if (component.MultipleLock && List.Any(item => item.GetType() == component.GetType()))
                throw new ArgumentException("This component cannot be added several time to the same entity ");
            return true;
        }
        protected override bool CanRemove(BaseComponent value)
        {
            return true;
        }


        public bool Remove<T>()
            where T:class
        {
            return Remove(List.Find(item => item is T));
        }
        public bool Has<T>()
            where T:class
        {
            return List.Any(item => item is T);
        }


    }
}
