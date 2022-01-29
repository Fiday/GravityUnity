using UnityEngine;

namespace _OpenXR.Scripts
{
    [System.Serializable]
    public class Tunes
    {
        [SerializeField] public float StartTime { get; set; }

        [SerializeField] public AudioClip Clip { get; set; }
    }
}