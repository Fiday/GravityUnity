using _OpenXR.Scripts;
using UnityEngine;

public class Sonification : MonoBehaviour
{
    [SerializeField] public bool Active { get; set; }

    private void OnTriggerExit(Collider other)
    {
        if (!Active) return;
        if (other.TryGetComponent(typeof(SoundScript), out Component component))
        {
            ((SoundScript) component).PlaySound();
            Debug.Log("soundplayed");
        }
        //other.gameObject.GetComponent<SoundScript>().PlaySound();
    }
}