using UnityEngine;

namespace _OpenXR.Scripts
{
    public class SoundScript : MonoBehaviour
    {
        public AudioClip sound;

        public void AddSound(string path)
        {
            sound = (AudioClip) Resources.Load(path);
            gameObject.AddComponent<AudioSource>();
            GetComponent<AudioSource>().clip = sound;
        }

        public void PlaySound()
        {
            if (sound != null)
            {
                GetComponent<AudioSource>().Play();
            }
        }
    }
}