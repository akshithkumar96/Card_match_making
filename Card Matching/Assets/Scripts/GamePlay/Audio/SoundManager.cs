using System;
using UnityEngine;

namespace CardMatching.Souds
{
    /// <summary>
    /// sound manager
    /// </summary>
    public class SoundManager : MonoBehaviour
    {
        //Audio pair class for serializing type withaudio
        [Serializable]
        private class AudioPair
        {
            public AudioType audioType;
            public AudioClip clip;
        }
        /// <summary>
        /// Audio pairs
        /// </summary>
        [SerializeField] AudioPair[] audioPairs;
        [SerializeField] AudioSource audioSource;

        private static SoundManager instance;

        public static SoundManager GetInstance
        {
            get
            {
                if (instance == null)
                {
                    // Find if there's an existing instance in the scene
                    instance = FindObjectOfType<SoundManager>();

                    // If no instance exists, create a new GameObject with SoundManager
                    if (instance == null)
                    {
                        //Throw an exception saying no audio manager
                        Debug.LogError("Plase add audio manager");
                    }
                }
                return instance;
            }
        }

        private void Awake()
        {
            if (instance == null)
            {
                //if instance is null set this as  instance
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                // If an instance already exists and it's not this one, destroy this one
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Play the auido
        /// </summary>
        /// <param name="audioType"> audio type to play</param>
        public void Play(AudioType audioType)
        {
            var clip = GetAudioClip(audioType);
            if (clip == null)
            {
                Debug.LogError("Audion cannot be null");
                return;
            }
            audioSource.clip = clip;
            audioSource.Play();
        }

        /// <summary>
        /// Get audio from audio type
        /// </summary>
        /// <param name="audioType">Audio type </param>
        /// <returns></returns>
        private AudioClip GetAudioClip(AudioType audioType)
        {
            foreach (var pair in audioPairs)
            {
                if (audioType == pair.audioType)
                    return pair.clip;
            }
            return null;
        }
    }

    /// <summary>
    /// game audio type
    /// </summary>
    public enum AudioType
    {
        Flip = 0,
        Match,
        MisMatch,
        GameOver 
    }
}