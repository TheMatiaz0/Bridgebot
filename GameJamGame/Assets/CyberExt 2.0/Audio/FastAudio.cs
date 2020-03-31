using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

namespace Cyberevolver.Unity
{
	public static class FastAudio
	{
        public static void PlayAtPoint(Vector2 point, AudioClip clip, AudioMixerGroup mixer)
        {
            GameObject g = new GameObject();
            var s = g.AddComponent<AudioSource>();
            s.transform.position = point;
            s.clip = clip;
            s.outputAudioMixerGroup = mixer;
            s.Play();
            s.gameObject.AddComponent<MonoBehaviourPlus>().StartCoroutine(Cor(s));
        }

        private static IEnumerator Cor(AudioSource audioSource)
        {
            yield return Async.NextFrame;
            yield return Async.Until(() => !audioSource.isPlaying);
            UnityEngine.Object.Destroy(audioSource.gameObject);

        }
    }
}

