using System;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    private float _currPullRadius;

    public bool DestroyOnContact { get; set; } = false;

    private void OnTriggerEnter(Collider other)
    {
        if (DestroyOnContact)
        {
            Destroy(other.gameObject);
        }
    }
}

