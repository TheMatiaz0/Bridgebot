using System;
using System.Collections.Generic;
using System.Linq;

namespace TrualityEngine.Core
{


	/// <summary>
	/// Base for <see cref="Event{T}"/>
	/// </summary>
	public abstract class BEvent
	{

		internal virtual BEvent BClone() { return null; }

	}
	public enum Event
	{
		Empty = 0
	}

	/// <summary>
	/// Truality event. 
	/// You can wait for the event in Coroutine or in async method using <see cref="Wait()"/>
    /// If subscriber implements <see cref="IKillable"/> his action will be remove when get killed.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Event<T> : BEvent, ICloneable
		where T : EventArgs
	{

        public Event<T> Add(EventHandler<T> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            _Value += action;
            return this;

        }


		private EventHandler<T> _Value = delegate { };
		/// <summary>
		/// Base event
		/// </summary>
		public event EventHandler<T> Value
		{
			add
			{
				_Value += value;
			}
			remove
			{
				_Value -= value;
			}
		}
		public Event()
		{

		}
		internal void Clear()
		{
			_Value = delegate { };
		}
		public void Invoke(object sender, T args)
		{
            foreach(var item in _Value.GetInvocationList().ToArray())
            {
                if (item.Target is IKillable k && k.IsKill)
                    _Value -=(System.EventHandler<T>) item;
                   
            }
            _Value?.Invoke(sender, args);



        }
		public ICoroutineable Wait()
		{

            return new WaitToId(BaseWait());

        }
		private uint BaseWait()
		{
			LastId++;
			static void action(object s, T e) => OnEvent(LastId);
			void deletedAction() => Value -= action;
			DeletedActions.Add(LastId, deletedAction);
			Value += action;
			return LastId;
		}


		public Event<T> Clone()
		{
			Event<T> ev = new Event<T>
			{
				_Value = this._Value.Clone() as EventHandler<T>
			};
			return ev;
		}
		/// <summary>
		/// It's important only for cloning system
		/// </summary>
		/// <returns></returns>
		internal override BEvent BClone()
		{
			return Clone();
		}
		object ICloneable.Clone()
		{
			return Clone();
		}


		private static Dictionary<uint, Action> DeletedActions { get; } = new Dictionary<uint, Action>();
		private static uint LastId { get; set; } = 0;

		private class WaitToId : ICoroutineable
		{
			public uint Id { get; }
			public WaitToId(uint id)
			{
				Id = id;
			}

			public bool GetIsDone()
			{
				return !DeletedActions.ContainsKey(Id);
			}
		}
		private static void OnEvent(uint id)
		{
			DeletedActions[id].Invoke();
			DeletedActions.Remove(id);
		}





		public static implicit operator Event<T>(Event @event)
		{
			if (@event == Event.Empty) return new Event<T>();
			else return null;
		}


	}
}
