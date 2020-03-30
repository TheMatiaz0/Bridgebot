using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
namespace TrualityEngine.Core
{
    /// <summary>
    /// DrawableBehviour which know when his entity do something
    /// </summary>
    public abstract class EntityCaller : Behaviour, IActivable
    {


        protected internal virtual void IfDraw(FixedBatch fixedBatch) { }

        internal int EditorId { get; set; }
        private bool IsStillWaitForSecondClick { get; set; }
        private const float WaitForDoubleClickTime = 0.9f;

        /// <summary>
        /// Return self active
        /// If you want to get active in hierachy, you will get IsActive property
        /// </summary>
      
        public bool IsActiveSelf
        {
            get => _ActiveSelf;
            set
            {

                Pair<bool, EntityCaller>[] toTest =
                    (from caller in GetToTestWhenActiveChaned().Union(new EntityCaller[] { this })
                     select new Pair<bool, EntityCaller>(caller.IsActive, caller)).ToArray();

                _ActiveSelf = value;
                foreach (Pair<bool, EntityCaller> element in toTest)
                {
                    bool before = element.Second.IsActive; //IsActive eat a little time;
                    if (element.First != before&&IsInit)
                        element.Second.IfActiveChange(before);
                }


            }
        }
      
        public EntityCaller Orignal { get; private set; } = null;

       
        private bool _ActiveSelf = true;
        /// <summary>
        /// It is calling, when mouse  colliding with Entity collider
        /// </summary>
        public Event<BaseColliderEntityArg> OnMouseOver { get; protected set; } = Event.Empty;
        /// <summary>
        /// It is calling, when mouse  colliding with Entity collider and this is first frame of colliding
        /// </summary>
        public Event<BaseColliderEntityArg> OnMouseStartOver { get; protected set; } = Event.Empty;

        /// <summary>
        /// It is calling, when mouse  colliding with Entity collider and mouse is pressing
        /// </summary>
        public Event<MouseCollisionArgs> OnMousePressed { get; protected set; } = Event.Empty;

        public Event<CollisonArgs> OnCollision { get; protected set; } = Event.Empty;
        public Event<CollisonArgs> OnStartCollision { get; protected set; } = Event.Empty;
        public Event<CollisonArgs> OnEndCollision { get; protected set; } = Event.Empty;
        public Event<CollisonArgs> OnStayCollision { get; protected set; } = Event.Empty;
        public Event<CameraWillRenderChangeArgs> OnCameraWillRenderChanged { get; protected set; } = Event.Empty;

        public Event<BaseEntityArgs> OnEntityEscapeFromCamera { get; protected set; } = Event.Empty;
        public Event<BaseEntityArgs> OnEntityBackToCamera { get; protected set; } = Event.Empty;
        public Event<MouseCollisionArgs> OnMouseSingleClick { get; protected set; } = Event.Empty;
        public Event<MouseCollisionArgs> OnMouseDoubleClick { get; protected set; } = Event.Empty;
        public Event<BaseEntityArgs> OnMouseEndOver { get; protected set; } = Event.Empty;
        public Event<MouseCollisionArgs> OnMouseDrag { get; protected set; } = Event.Empty;
      
    
        internal void IfMouseOver_() => IfMouseOver();
        internal void IfMousePressed_(MouseButton mouseButton,State state) => IfMouseIsPressing(mouseButton ,state);
        internal void IfMouseStartOver_() => IfMouseStartOver();
        internal void IfMouseEndOver_() => IfMouseEndOver();
        internal void IfMouseDrag_(MouseButton mouseButton,State state) => IfMouseDrag( mouseButton,state);

        internal void IfEntityWillRenderIsChanged_(bool to) => IfEntityWillRenderIsChanged( to);

        internal void IfCollision_(ColliderEntity collider,State state) => IfCollision(collider,state);
      

    
     
        /// <summary>
        /// It is calling, when mouse  colliding wich Entity collider
        /// </summary>
        protected virtual void IfMouseOver()
        {
            OnMouseOver.Invoke(this, new BaseColliderEntityArg((GetFrom() as ColliderEntity)));
        }


        /// <summary>
        /// It is calling, when mouse  colliding with Entity collider and mouse is pressing
        /// </summary>
        protected virtual void IfMouseIsPressing(MouseButton mouseButton,State state)
        {
            OnMousePressed.Invoke(this, new MouseCollisionArgs(GetFrom() as ColliderEntity,state,mouseButton));
            if (state == State.FirstFrame)
                IfMouseSingleClick(mouseButton);
            
        }
        protected virtual void IfMouseDrag(MouseButton mouseButton, State state)
        {
            OnMouseDrag.Invoke(this, new MouseCollisionArgs(GetFrom() as ColliderEntity, state, mouseButton));

        }
       private Coroutine WaitingForDoubleClickCoroutine { get; set; }
        bool IActivable.IsActive
        {
            get => IsActiveSelf;
            set => IsActiveSelf = value;
        }

        
        protected virtual void IfMouseSingleClick(MouseButton mouseButton)
        {
            if (IsStillWaitForSecondClick)
            {
                WaitingForDoubleClickCoroutine?.Cancel();
                IfMouseDoubleClick(mouseButton);
                IsStillWaitForSecondClick = false;
            }
               
            else
            {
                IsStillWaitForSecondClick = true;              
                WaitingForDoubleClickCoroutine?.Cancel();
                WaitingForDoubleClickCoroutine = Yield.Start(WaitForDoubleClick());
            }
              
            OnMouseSingleClick.Invoke(this, new MouseCollisionArgs(GetFrom() as ColliderEntity,State.FirstFrame, mouseButton));
          
            
        }
        protected IEnumerator<ICoroutineable> WaitForDoubleClick()
        {
          
            yield return Async.WaitTime(WaitForDoubleClickTime);
            IsStillWaitForSecondClick = false;
        }
        protected virtual void IfMouseDoubleClick(MouseButton mouseButton)
        {
            OnMouseDoubleClick.Invoke(this, new MouseCollisionArgs(GetFrom() as ColliderEntity,State.FirstFrame,mouseButton));
        }
        

        protected virtual void IfEntityWillRenderIsChanged(bool to)
        {
            OnCameraWillRenderChanged.Invoke(this, new CameraWillRenderChangeArgs(GetFrom() as Entity, to));
            if (to)
                IfEntityBackToCamera();
            else
                IfEntityEscapeFromCamera();

        }
        protected virtual void IfEntityEscapeFromCamera()
        {
            OnEntityEscapeFromCamera.Invoke(this, new BaseEntityArgs(GetFrom()));
        }
        protected virtual void IfEntityBackToCamera()
        {
            OnEntityBackToCamera.Invoke(this, new BaseEntityArgs(GetFrom()));
        }

        /// <summary>
        /// It is calling, when mouse  colliding with Entity collider and this is first frame of colliding
        /// </summary>

        protected virtual void IfMouseStartOver()
        {
            OnMouseStartOver.Invoke(this, new BaseColliderEntityArg( (GetFrom() as ColliderEntity)));
        }
        /// <summary>
        /// It is calling, when mouse does not colliding with entity, but mouse has collided in last frame
        /// </summary>
        protected virtual void IfMouseEndOver()
        {
            OnMouseEndOver.Invoke(this, new BaseEntityArgs((GetFrom() as ColliderEntity)));
        }

        /// <summary>
        /// It is calling , when something colliding with entity
        /// </summary>
        /// <param name="collider"></param>
        protected virtual void IfCollision(ColliderEntity collider,State state)
        {
            OnCollision.Invoke(this, new CollisonArgs((GetFrom() as ColliderEntity), (collider as ColliderEntity)));
            if (state == State.FirstFrame)
                IfCollisionStart(collider);
            if (state == State.Up)
                IfCollisionEnd(collider);
            if (state != State.None && state != State.Up)
                IfCollisionStay(collider, state);

        }
        protected virtual void IfCollisionStart(ColliderEntity collider)
        {
            OnStartCollision.Invoke(this, new CollisonArgs((GetFrom() as ColliderEntity), (collider as ColliderEntity)));
        }
        protected virtual void IfCollisionEnd(ColliderEntity collider)
        {
            OnEndCollision.Invoke(this, new CollisonArgs((GetFrom() as ColliderEntity), (collider as ColliderEntity)));
        }
        protected virtual void IfCollisionStay(ColliderEntity collider,State state)
        {
            OnStayCollision.Invoke(this, new CollisonArgs((GetFrom() as ColliderEntity), (collider as ColliderEntity)));
        }

        public abstract Entity GetFrom();
        protected abstract IEnumerable<EntityCaller> GetToTestWhenActiveChaned();







    }

}
