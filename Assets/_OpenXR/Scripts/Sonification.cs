using System;
using System.Collections;
using System.Collections.Generic;
using _OpenXR.Scripts;
using UnityEngine;

public class Sonification : MonoBehaviour
{
    public bool Active { get; set; } = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerExit(Collider other)
    {
        if (Active)
        {
            if (other.TryGetComponent(typeof(SoundScript), out Component component))
            {
                ((SoundScript) component).PlaySound();
            }
            //other.gameObject.GetComponent<SoundScript>().PlaySound();
        }
    }
}