using System;

namespace TrualityEngine.Core
{
    public enum State
    {
        None = 0,
        FirstFrame,
        Down,
        Up,

    }
    public class Stater
    {
        public Stater(State state)
        {
            State = state;
        }

        public State State { get; private set; }
        public void AddValue(bool value)
        {
            State = GetValueAfterChange(value, State);
        }
        public static State GetValueAfterChange(bool pressed, State state)
        {
            if (pressed == true)
            {
                switch (state)
                {

                    case State.Up: case State.None: return State.FirstFrame;
                    case State.Down: case State.FirstFrame: return State.Down;

                }
            }
            else
            {
                switch (state)
                {
                    case State.None: case State.Up: return State.None;
                    case State.FirstFrame: case State.Down: return State.Up;

                }

            }
            throw new ArgumentException("Unknown value");

        }

    }
}
