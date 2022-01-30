using System;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    private float _currPullRadius;

    public bool DestroyOnContact { get; set; }

    [SerializeField] private float _minWeight;

    public float MinWeight
    {
        get => _minWeight;
        set => _minWeight = value;
    }

    [SerializeField] private float _maxWeight;

    public float MaxWeight
    {
        get => _maxWeight;
        set => _maxWeight = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (DestroyOnContact)
        {
            Destroy(other.gameObject);
        }
    }

    public void ChangeWeight(float scalingFactor)
    {
            var attractionComp = GetComponent<AttractionComponent>();
            var temp = attractionComp.Mass;

            temp *= 1 + scalingFactor;
            attractionComp.Mass = Mathf.Clamp(temp, _minWeight, _maxWeight);
    }
}