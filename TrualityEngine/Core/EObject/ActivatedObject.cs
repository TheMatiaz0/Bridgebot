
namespace TrualityEngine.Core
{
    /// <summary>
    /// ActivatedObject knows when the time comes for it to be activated and disabled
    /// </summary>
    public abstract class ActivatedObject : EObject
    {

        public Event<ActiveChangeArgs> OnActivateChange { get; protected set; } = Event.Empty;
        /// <summary>
        /// It is called, when active is set from false to true
        /// </summary>
        public Event<BaseActivatedObjectArgs> OnEnable { get; protected set; } = Event.Empty;
        /// <summary>
        /// It is called, when active is set from true to false
        /// </summary>
        public Event<BaseActivatedObjectArgs> OnDisable { get; protected set; } = Event.Empty;


     
        public ActivatedObject()
        {

        }

        protected void CallEventOnActiveChange(bool value)
            => OnActivateChange.Invoke(this, new ActiveChangeArgs(this,value));
        protected virtual void IfActiveChange(bool value)
        {
            OnActivateChange.Invoke(this, new ActiveChangeArgs(this, value));
            if (value)
                IfEnable();
            else
                IfDisable();
        }
        protected virtual void IfEnable()
        {
            OnEnable.Invoke(this, new BaseActivatedObjectArgs(this));
        }
        protected virtual void IfDisable()
        {
            OnDisable.Invoke(this, new BaseActivatedObjectArgs(this));
        }

    }
}
