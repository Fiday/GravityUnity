using System;
using UnityEngine;

namespace _OpenXR.Scripts
{
    public class SoundScript : MonoBehaviour
    {
        public AudioClip sound;

        private void Start()
        {
            AddSound("");
        }

        public void AddSound(string path)
        {
           // sound = (AudioClip) Resources.Load(path);
            gameObject.AddComponent<AudioSource>();
            GetComponent<AudioSource>().clip = sound;
        }

        public void PlaySound()
        {
            if (sound != null)
            {
                var audiosource = GetComponent<AudioSource>();
                    //audiosource.volume = size und so 
                    audiosource.Play();
            }
        }
    }
}