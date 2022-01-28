using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TextUpdate : MonoBehaviour
{
    
    private float previousValue;

    private TextMeshPro textmeshMass;

    private AttractionComponent attractionComponent;

    private SliderScript _sliderScript;

    // Start is called before the first frame update
    void Start()
    {
        textmeshMass = GameObject.Find("ValueMass").GetComponent<TextMeshPro>();
        attractionComponent = GameObject.FindWithTag("Blackhole").GetComponent<AttractionComponent>();
        textmeshMass.text = attractionComponent.Mass.ToString("0.#e+0");
        
        /*_sliderScript = GetComponentInChildren<SliderScript>();
        _sliderScript.SetCurrentValue(GetScaledValue(attractionComponent.Mass, attractionComponent.MinWeight,
            attractionComponent.MaxWeight, 0, 1));
        previousValue = _sliderScript.GetCurrentValue();*/
    }

    void Update()
    {
        /*var currentValue = GameObject.FindGameObjectWithTag("Body").GetComponent<SliderScript>().GetCurrentValue();
        Debug.Log($"currentvalue {currentValue}");
        if (Math.Abs(currentValue - previousValue) > 0.01)
        {
            attractionComponent.Mass = GetScaledValue(currentValue, 0, 1, attractionComponent.MinWeight,
                attractionComponent.MaxWeight);

            textmeshMass.text = attractionComponent.Mass.ToString("0.#e+0");
            previousValue = currentValue;
        }

        Debug.Log(attractionComponent.Mass);*/
    }

   float GetScaledValue(float x, float min, float max, float newMin, float newMax)
    {
        return (((newMax - newMin) * (x - min)) / (max - min)) + newMin;
    }
}