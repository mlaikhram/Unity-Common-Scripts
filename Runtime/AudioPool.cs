using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mlaikhram.Common
{

    [RequireComponent(typeof(AudioSource))]
    public class AudioPool : MonoBehaviour
    {
        /// <summary>
        /// The max number of audio sources this AudioPool can play at once.
        /// </summary>
        public uint poolSize;

        /// <summary>
        /// The max number of audio sources this AudioPool can play at once.
        /// </summary>
        public uint PoolSize => poolSize;

        internal class AudioPoolObject
        {
            public AudioSource audioSource;
            public Transform targetTransform;

            public AudioPoolObject(AudioSource audioSource)
            {
                this.audioSource = audioSource;
                this.targetTransform = null;
            }
        }

        private AudioSource rootAudioSource;
        private List<AudioPoolObject> audioPool;
        private int currentIndex;

        private void Awake()
        {
            rootAudioSource = GetComponent<AudioSource>();
            audioPool = new List<AudioPoolObject>();
            for (int i = 0; i < poolSize; ++i)
            {
                Debug.Log("i: " + i);
                GameObject go = new GameObject("pool object " + i);
                go.transform.position = transform.position;
                go.transform.SetParent(transform);
                AudioSource audioSource = go.CopyComponent(rootAudioSource);
                audioPool.Add(new AudioPoolObject(audioSource));
            }
        }


        private void Update()
        {
            foreach (AudioPoolObject audioPoolObject in audioPool)
            {
                if (audioPoolObject.targetTransform != null)
                {
                    audioPoolObject.audioSource.gameObject.transform.position = audioPoolObject.targetTransform.position;
                }
            }
        }

        /// <summary>
        /// Play an audio clip at the AudioPool's position.
        /// </summary>
        /// <param name="audio">The Audio Clip to play. Defaults to the Audio Clip in the attached Audio Source Component.</param>
        public void PlaySound(AudioClip audio = null)
        {
            audioPool[currentIndex].audioSource.clip = audio == null ? rootAudioSource.clip : audio;
            audioPool[currentIndex].targetTransform = null;
            audioPool[currentIndex].audioSource.Stop();
            audioPool[currentIndex].audioSource.gameObject.transform.position = transform.position;
            audioPool[currentIndex].audioSource.Play();

            currentIndex = (currentIndex + 1) % audioPool.Count;
        }

        /// <summary>
        /// Play an audio clip at a specific position.
        /// </summary>
        /// <param name="position">The position to play the audio from.</param>
        /// <param name="audio">The Audio Clip to play. Defaults to the Audio Clip in the attached Audio Source Component.</param>
        public void PlaySoundAt(Vector3 position, AudioClip audio = null)
        {
            audioPool[currentIndex].audioSource.clip = audio == null ? rootAudioSource.clip : audio;
            audioPool[currentIndex].targetTransform = null;
            audioPool[currentIndex].audioSource.Stop();
            audioPool[currentIndex].audioSource.gameObject.transform.position = position;
            audioPool[currentIndex].audioSource.Play();

            currentIndex = (currentIndex + 1) % audioPool.Count;
        }

        /// <summary>
        /// Play an audio clip as a specific transform.
        /// </summary>
        /// <param name="transform">The transform to play the audio as.</param>
        /// <param name="audio">The Audio Clip to play. Defaults to the Audio Clip in the attached Audio Source Component.</param>
        public void PlaySoundAs(Transform targetTransform, AudioClip audio = null)
        {
            audioPool[currentIndex].audioSource.clip = audio == null ? rootAudioSource.clip : audio;
            audioPool[currentIndex].targetTransform = targetTransform;
            audioPool[currentIndex].audioSource.Stop();
            audioPool[currentIndex].audioSource.gameObject.transform.position = targetTransform.position;
            audioPool[currentIndex].audioSource.Play();

            currentIndex = (currentIndex + 1) % audioPool.Count;
        }
    }
}