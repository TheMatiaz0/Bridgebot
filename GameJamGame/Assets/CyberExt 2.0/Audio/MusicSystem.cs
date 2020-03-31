using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

namespace Cyberevolver.Unity
{
    public class MusicSystem
    {
        private static CancellationTokenSource cancelation = new CancellationTokenSource();
        private enum VolumeDirection
        {
            Up = 0,
            Down = 1,
        }

        public static AudioSource AudioSourceInstance { get; set; }
        private static VolumeClip _Actual;
        public static VolumeClip Actual
        {
            get => _Actual;
            set
            {

                AudioSourceInstance.clip = value?.AudioClip;
                _Actual = value;
            }
        }
        public static float FadeEffectSpeed { get; set; } = 5;
        private static bool IfInstanceNullCreate()
        {


            if (object.ReferenceEquals(AudioSourceInstance, null))
            {
                GameObject gameObject = new GameObject
                {
                    name = "MusicSource"
                };
                AudioSourceInstance = gameObject.AddComponent<AudioSource>();
                AudioSourceInstance.outputAudioMixerGroup = (Resources.Load("Audio") as AudioMixer).FindMatchingGroups("Music")[0];
                MonoBehaviour.DontDestroyOnLoad(gameObject);


            }
            else if (AudioSourceInstance == false)
            {
                return false;
            }
            return true;
        }

        private static async Task SlowlyChangeMusicVolume(VolumeDirection dir, VolumeClip vClip, CancellationToken token)
        {

            while (AudioSourceInstance == false || (token.IsCancellationRequested == false && ((dir == VolumeDirection.Down) ? AudioSourceInstance.volume > 0 : AudioSourceInstance.volume < vClip.Volume.AsFloatValue - 0.05f)))
            {
                await new WaitForEndOfFrame();

                AudioSourceInstance.volume += ((dir == VolumeDirection.Down) ? -0.5f : 0.5f) * Time.unscaledDeltaTime * vClip.Volume.AsFloatValue * (FadeEffectSpeed);

            }


        }

        public static async Task ChangeMusicAsync(VolumeClip vClip)
        {

            Cancel();
            if (IfInstanceNullCreate() == false)
                return;

            if (vClip == null)
            {

                await SlowlyMute();

                Actual = null;
                return;
            }
            await Async.NextFrame;
            Actual = vClip;
            CancellationToken token = cancelation.Token;
            await new WaitForEndOfFrame();

            if (Actual != null)
                await SlowlyChangeMusicVolume(VolumeDirection.Down, vClip, token);
            else
                AudioSourceInstance.volume = 0;

            AudioSourceInstance.Play();
            await SlowlyChangeMusicVolume(VolumeDirection.Up, vClip, token);

        }
        public static void ChangeInThisMoment(VolumeClip vClip)
        {
            Cancel();
            if (IfInstanceNullCreate() == false)
                return;

            Actual = vClip;
            AudioSourceInstance.volume = vClip.Volume.AsFloatValue;

            AudioSourceInstance.Play();


        }
        public static async Task SlowlyUnMute()
        {
            Cancel();
            if (IfInstanceNullCreate() == false)
                return;

            await SlowlyChangeMusicVolume(VolumeDirection.Up, new VolumeClip(Actual.AudioClip, Actual.Volume), cancelation.Token);
        }
        private static void Cancel()
        {
            cancelation?.Cancel();
            cancelation = new CancellationTokenSource();
        }
        public static async Task SlowlyMute()
        {
            Cancel();
            if (IfInstanceNullCreate() == false)
                return;


            if (Actual == null)
                return;
            await SlowlyChangeMusicVolume(VolumeDirection.Down, new VolumeClip(Actual.AudioClip, AudioSourceInstance.volume), cancelation.Token);

        }


    }
}

