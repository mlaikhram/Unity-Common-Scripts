using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mlaikhram.Common
{
    [RequireComponent(typeof(AudioSource))]
    public class BackgroundMusic : MonoBehaviour
    {
        private static BackgroundMusic instance;

        protected AudioSource audioSource;

        /// <summary>
        /// The audio source used to play background music.
        /// </summary>
        public static AudioSource AudioSource => instance == null ? null : instance.audioSource;


        /// <summary>
        /// Whether or not the background music is currently playing.
        /// </summary>
        public static bool IsPlaying => instance == null ? false : instance.audioSource.isPlaying;

        private float defaultVolume;
        private float decayRate;

        // Start is called before the first frame update
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            defaultVolume = audioSource.volume;
            decayRate = 0f;
            if (instance != null && instance != this)
            {
                if (audioSource.clip != instance.audioSource.clip || !instance.audioSource.isPlaying)
                {
                    ChangeAudio(audioSource.clip, audioSource.loop);
                }
                Debug.Log("destroying self");
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }

        private void OnDestroy()
        {
            if (this == instance)
            {
                instance = null;
            }
        }

        private void Update()
        {
            transform.position = Camera.main.transform.position;
            if (decayRate > 0f)
            {
                audioSource.volume -= decayRate * Time.deltaTime;
                if (audioSource.volume <= 0f)
                {
                    audioSource.Stop();
                    audioSource.volume = defaultVolume;
                    decayRate = 0f;
                }
            }
        }

        /// <summary>
        /// Stop the current background music.
        /// </summary>
        /// <param name="fadeTime">How long it takes for the music to fade out. Defaults to 0.</param>
        public static void Stop(float fadeTime = 0f)
        {
            if (instance != null)
            {
                instance.decayRate = instance.audioSource.volume / fadeTime;
                // instance.audioSource.Stop();
            }
            else
            {
                Debug.LogError("Trying to access nonexistant BackgroundMusic. BackgroundMusic might not have been added to the scene or might have been destroyed.");
            }
        }

        /// <summary>
        /// Play the current background music.
        /// </summary>
        /// <param name="volume">What volume to play the background music at. Defaults to initial volume.</param>
        public static void Play(float volume = 0f)
        {
            if (instance != null)
            {
                instance.audioSource.volume = volume > 0f ? volume : instance.defaultVolume;
                instance.audioSource.Play();
            }
            else
            {
                Debug.LogError("Trying to access nonexistant BackgroundMusic. BackgroundMusic might not have been added to the scene or might have been destroyed.");
            }
        }

        /// <summary>
        /// Change the background music audio.
        /// </summary>
        /// <param name="audioClip">The new Audio Clip to play.</param>
        /// <param name="loop">Whether or not to loop the new audio. Defaults to true.</param>
        public static void ChangeAudio(AudioClip audioClip, bool loop = true)
        {
            if (instance != null)
            {
                instance.audioSource.Stop();
                instance.audioSource.volume = instance.defaultVolume;
                instance.decayRate = 0f;

                instance.audioSource.clip = audioClip;
                instance.audioSource.loop = loop;
                instance.audioSource.Play();
            }
            else
            {
                Debug.LogError("Trying to access nonexistant BackgroundMusic. BackgroundMusic might not have been added to the scene or might have been destroyed.");
            }
        }
    }
}