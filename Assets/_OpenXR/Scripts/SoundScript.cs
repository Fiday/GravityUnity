using System;
using UnityEngine;

namespace _OpenXR.Scripts
{
    public class SoundScript : MonoBehaviour
    {
        public void AddSound(AudioClip sound)
        {
            gameObject.AddComponent<AudioSource>();
            GetComponent<AudioSource>().clip = sound;
        }

        public void PlaySound()
        {
            if (TryGetComponent(typeof(AudioSource), out Component component))
            {
                var source = (AudioSource) component;
                source.volume = transform.localScale.x * 2;
                source.Play();
            }
        }
    }
}