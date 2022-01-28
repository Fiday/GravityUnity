using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextUpdate : MonoBehaviour
{
    private float previousValue;

    private TextMeshPro textmesh;

    private AttractionComponent attractionComponent;

    // Start is called before the first frame update
    void Start()
    {
        textmesh = GetComponent<TextMeshPro>();
        attractionComponent = GameObject.FindWithTag("Blackhole").GetComponent<AttractionComponent>();
        textmesh.text = attractionComponent.Mass.ToString("0.#e+0");
        previousValue = GetComponentInChildren<SliderScript>().GetCurrentValue();
    }

    // Update is called once per frame
    void Update()
    {
     //  var currentValue = GetComponentInChildren<SliderScript>().GetCurrentValue();
     //  if (Math.Abs(currentValue - previousValue) > 0.01)
     //  {
     //      attractionComponent.Mass = attractionComponent.MinWeight +
     //                                 ((attractionComponent.MaxWeight * currentValue) - attractionComponent.MinWeight);
     //      textmesh.text = attractionComponent.Mass.ToString("0.#e+0");
     //      previousValue = currentValue;
     //  }
    }
}