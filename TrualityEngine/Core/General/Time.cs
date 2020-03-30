using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
	/// <summary>
	/// Main time in gameplay information
	/// </summary>
	public  class TimeOfGame:ICloneable
	{

        public static TimeOfGame Actual { get; private set; } = new TimeOfGame();
		private  float _GlobalTimeScale = 1f;

		/// <summary>
		/// Number of actual frame
		/// </summary>
		public  ulong FrameQuanity { get; private set; } = 0;
		/// <summary>
		/// Total game time, including time scaling
		/// </summary>
		public  TimeSpan TotalTime { get; private set; } = new TimeSpan();
        public  TimeSpan UnscaledTotalTime { get; private set; }= new TimeSpan();
		/// <summary>
		/// Time between this frame and last frame, including time scaling
		/// </summary>
		public  TimeSpan DeltaTime { get; private set; } = new TimeSpan();
        public  TimeSpan UnscaledDeltaTime { get; private set; } = new TimeSpan();
        public  TimeSpan FixedUpdateDelay { get; set; } = TimeSpan.FromSeconds(0.01f);
		public  float DeltaTimeAsFloat => (float)DeltaTime.TotalSeconds;
		/// <summary>
		/// Representation of global time scaling, if you want set TimeScale of scene you have to use SceneRuntime.TimeScale
		/// </summary>
		public  float GlobalTimeScale
		{
			get => _GlobalTimeScale;
			set
			{
				if (value < 0)
					throw new ArgumentException($"{nameof(value)} cannot be lesser then 0", nameof(value));
				_GlobalTimeScale = value;
			}
		}
		/// <summary>
		/// Includes GlobalTimeScale and actual scene time scale
		/// </summary>
		/// <returns></returns>
		public  float GetFullTimeScale()
			=> GlobalTimeScale * SceneRuntime.ActualScene?.TimeScale ?? 1f;
		public  double Fps => 1.0 / DeltaTime.TotalSeconds;

		internal  void NextFrame(TimeSpan deltaTime)
		{
			FrameQuanity++;
			TimeSpan realTimeDelta = TimeSpan.FromTicks((long)(deltaTime.Ticks * GetFullTimeScale()));
			DeltaTime = realTimeDelta;
			TotalTime += realTimeDelta;
            UnscaledTotalTime += deltaTime;
            UnscaledDeltaTime = deltaTime;

		}
        public TimeOfGame Clone()
        {
            return (TimeOfGame)this.MemberwiseClone();
        }
        object ICloneable.Clone()
        {
           return Clone();
        }
    }
}
