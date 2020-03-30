using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    /// <summary>
    /// Static class which has information about user input
    /// </summary>
    public class Input : ICloneable
    {
        public static Input Actual { get; private set; } = new Input();
        private Dictionary<Keys, State> StatesKeys { get; } = new Dictionary<Keys, State>();
        private Dictionary<MouseButton, State> MouseKeys { get; } = new Dictionary<MouseButton, State>();
        private Dictionary<GamePadButton, State>[] GamePadPlayers { get; } = new Dictionary<GamePadButton, State>[4];
        private float MouseWheelInLastFrame { get; set; } = 0;
        /// <summary>
        /// Returns simple value from -4 to 4 
        /// </summary>
        private float BaseMouseWheel { get; set; } = 0;
        /// <summary>
        /// Mouse wheel value is fluided
        /// </summary>
        public float MouseWheel { get; private set; } = 0;
        /// <summary>
        /// Set or get MouseVisible.
        /// </summary>
        public bool MouseVisible
        {
            get => GameHeart.Actual.BaseGame.IsMouseVisible;
            set => GameHeart.Actual.BaseGame.IsMouseVisible = value;
        }
        /// <summary>
        /// Represents mouse position relative to the virtual position
        /// </summary>

        public Vect2 MousePosition { get; private set; }

        private Direction[] leftThumbStick = new Direction[4];
        private Direction[] rightThumbStick = new Direction[4];
        internal Input()
        {
            GenerateAll(StatesKeys);
            GenerateAll(MouseKeys);
            for (int i = 0; i < 4; i++)
            {
                GamePadPlayers[i] = new Dictionary<GamePadButton, State>();
                GenerateAll(GamePadPlayers[i]);

            }


            static void GenerateAll<T>(Dictionary<T, State> collection)
            {
                foreach (T value in Enum.GetValues(typeof(T)).Cast<T>())
                    collection[value] = State.None;
            }

        }

        internal void InputUpdate()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            GlobalUpdate(StatesKeys, keyboardState.IsKeyDown);
            int i = 0;
            foreach (var gamePadKeys in GamePadPlayers)
            {
                GamePadState gamePadState = GamePad.GetState(i);
                GlobalUpdate(gamePadKeys, (k) => gamePadState.IsButtonDown((Buttons)k));
                leftThumbStick[i] = new Direction(gamePadState.ThumbSticks.Left);
                rightThumbStick[i] = new Direction(gamePadState.ThumbSticks.Right);
                i++;
            }


            static void GlobalUpdate<T>(Dictionary<T, State> collection, Predicate<T> predicate)
            {
                foreach (T value in collection.Keys.ToArray())
                    collection[value] = Stater.GetValueAfterChange(predicate(value), collection[value]);
            }



            MouseState mouseState = Mouse.GetState();
            Vect2 pos = mouseState.Position.ToVector2();
            Vect2 s = (Screen.Active.Resolution / Screen.Virtual);
            pos = pos.Transform(Matrix.Invert(Matrix.CreateScale(s.X, s.Y, 1)));
            MousePosition = (new Vect2(pos.X, Screen.Virtual.ToVect2().Y - pos.Y) - Screen.Virtual.ToVect2() / 2) + Camera.Main?.GetCenter() ?? new Vect2(0, 0);
            MouseUpdate(MouseButton.Left, mouseState.LeftButton);
            MouseUpdate(MouseButton.Middle, mouseState.MiddleButton);
            MouseUpdate(MouseButton.Right, mouseState.RightButton);

            BaseMouseWheel = Mouse.GetState().ScrollWheelValue / 120.0f - MouseWheelInLastFrame;
            MouseWheelInLastFrame = Mouse.GetState().ScrollWheelValue / 120.0f;


            if (BaseMouseWheel == 0f && Math.Abs(MouseWheel) > 0.1f)
            {
                float val = 4f * (float)TimeOfGame.Actual.DeltaTime.TotalSeconds;
                if (MouseWheel > 0)
                    MouseWheel -= val;
                else
                    MouseWheel += val;

            }
            else
                MouseWheel = BaseMouseWheel;

            void MouseUpdate(MouseButton button, ButtonState state)
            {
                bool isPressed = state == ButtonState.Pressed;
                MouseKeys[button] = Stater.GetValueAfterChange(isPressed, MouseKeys[button]);
            }

        }

        /// <summary>
        /// Have user been pressing specific key?
        /// </summary>
        /// <param name="key">Specific key.</param>
        /// <returns></returns>
        public bool IsPressed(MouseButton key)
        {
            if (!GameHeart.Actual.BaseGame.IsActive)
                return false;

            return MouseKeys[key] == State.FirstFrame || MouseKeys[key] == State.Down;
        }
        /// <summary>
        /// Have user start pressing specific key in this frame?
        /// </summary>
        /// <param name="key">Specific key.</param>
        /// <returns></returns>
        public bool IsFirstFrame(MouseButton key)
        {
            return MouseKeys[key] == State.FirstFrame;
        }
        /// <summary>
        /// Have user stopped to press specific key in this frame?
        /// </summary>
        /// <param name="key">Specific key.</param>
        /// <returns></returns>
        public bool IsEndOfPress(MouseButton key)
        {
            return MouseKeys[key] == State.Up;
        }
        /// <summary>
        /// Have NOT user been pressing specific key in this frame?
        /// </summary>
        /// <param name="key">Specific key.</param>
        /// <returns></returns>
        public bool IsUnpressed(MouseButton key)
        {
            return !IsPressed(key);
        }
        /// <summary>
        /// Get more information about specific key in this frame.
        /// </summary>
        /// <param name="key">Specific key.</param>
        /// <returns></returns>
        public State GetState(MouseButton key)
        {
            return MouseKeys[key];
        }

        /// <summary>
        /// Have user been pressing specific key?
        /// </summary>
        /// <param name="key">Specific key.</param>
        /// <returns></returns>
        public bool IsPressed(Keys key)
        {
            return StatesKeys[key] == State.FirstFrame || StatesKeys[key] == State.Down;
        }
        /// <summary>
        /// Get more information about specific key in this frame.
        /// </summary>
        /// <param name="key">Specific key.</param>
        /// <returns></returns>
        public State GetKeyState(Keys key)
        {
            return StatesKeys[key];
        }
        /// <summary>
        /// Have user start pressing specific key in this frame?
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsFirstFrame(Keys key)
        {
            return StatesKeys[key] == State.FirstFrame;
        }
        /// <summary>
        /// Have user stopped to press specific key in this frame?
        /// </summary>
        /// <param name="key">Specific key.</param>
        /// <returns></returns>
        public bool IsEndOfPress(Keys key)
        {
            return StatesKeys[key] == State.Up;
        }
        /// <summary>
        /// Have NOT user been pressing specific key in this frame?
        /// </summary>
        /// <param name="key">Specific key.</param>
        /// <returns></returns>
        public bool IsUnpressed(Keys key)
        {
            return !IsPressed(key);
        }

        public void SetMouseCursorVisibility (bool isTrue)
        {
            GameHeart.Actual.BaseGame.IsMouseVisible = isTrue;
        }

        /// <summary>
        /// Have user been pressing specific key?
        /// </summary>
        /// <param name="key"></param>
        /// <param name="player">GamePad number. Value should be in range 0-3.</param>
        /// <returns></returns>
        public bool IsPressed(GamePadButton key, byte player)
        {
            return GamePadPlayers[player][key] == State.FirstFrame || GamePadPlayers[player][key] == State.Down;
        }
        /// <summary>
        /// Get more information about specific key in this frame.
        /// </summary>
        /// <param name="key">Specific key.</param>
        /// <param name="player">GamePad number. Value should be in range 0-3.</param>
        /// <returns></returns>
        public State GetKeyState(GamePadButton key, byte player)
        {
            return GamePadPlayers[player][key];
        }
        /// <summary>
        /// Have user start pressing specific key in this frame?
        /// </summary>
        /// <param name="key"></param>
        /// <param name="player">GamePad number. Value should be in range 0-3.</param>
        /// <returns></returns>
        public bool IsFirstFrame(GamePadButton key, byte player)
        {
            return GamePadPlayers[player][key] == State.FirstFrame;
        }
        /// <summary>
        /// Have user stopped to press specific key in this frame?
        /// </summary>
        /// <param name="key">Specific key.</param>
        /// <param name="player">GamePad number. Value should be in range 0-3.</param>
        /// <returns></returns>
        public bool IsEndOfPress(GamePadButton key, byte player)
        {
            return GamePadPlayers[player][key] == State.Up;
        }
        /// <summary>
        /// Have NOT user been pressing specific key in this frame?
        /// </summary>
        /// <param name="key">Specific key.</param>
        /// <param name="player">GamePad number. Value should be in range 0-3.</param>
        /// <returns></returns>
        public bool IsUnpressed(GamePadButton key, byte player)
        {
            return !IsPressed(key, player);
        }
        public Direction GetLeftThumbStick(byte player)
        {
            return leftThumbStick[player];
        }
        public Direction GetRightThumbStick(byte player)
        {
            return rightThumbStick[player];
        }
        /// <summary>
        /// Gets all pressed keys on keyboard. 
        /// It can take a little amount of time.
        /// </summary>
        /// <returns></returns>
        public Keys[] GetAllPressedKeyboardKey()
        {
            return StatesKeys.Where((item) => IsPressed(item.Key)).Select(item => item.Key).ToArray();
        }
        /// <summary>
        /// Gets all pressed buttons on game pad. 
        /// It can take a little amount of time.
        /// </summary>
        /// <returns></returns>
        public GamePadButton[] GetAllPressedGamePadButtons(byte player)
        {
            return GamePadPlayers[player].Where((item) => IsPressed(item.Key, player)).Select(item => item.Key).ToArray();
        }
        /// <summary>
        /// All instructs after that in the frame will detect given key was pressed
        /// </summary>
        /// <param name="key"></param>
        public void Simulate(Keys key, bool value)
        {
            Simulate(key, StatesKeys, value);
        }
        /// <summary>
        /// All instructs after that in the frame will detect given button was pressed
        /// </summary>
        /// <param name="button"></param>
        /// <param name="player"></param>
        public void Simulate(GamePadButton button, bool value, byte player)
        {
            Simulate<GamePadButton>(button, GamePadPlayers[player], value);
        }
        private void Simulate<T>(T key, Dictionary<T, State> collection, bool value)
        {
            collection[key] = Stater.GetValueAfterChange(value, collection[key]);
        }
        public Input Clone()
        {
            return (Input)(MemberwiseClone());
        }
        object ICloneable.Clone()
        {
            return Clone();
        }

    }
}
