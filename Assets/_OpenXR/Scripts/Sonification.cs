using System;
using System.Collections;
using System.Collections.Generic;
using _OpenXR.Scripts;
using UnityEngine;

public class Sonification : MonoBehaviour
{
    public bool Active { get; set; }
    
    private void OnTriggerExit(Collider other)
    {
        if (!Active) return;
        if (other.TryGetComponent(typeof(SoundScript), out Component component))
        {
            ((SoundScript)component).PlaySound();
        }
        //other.gameObject.GetComponent<SoundScript>().PlaySound();
    }
}