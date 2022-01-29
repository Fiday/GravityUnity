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

    public void UpdateTextMesh()
   {
       textmeshMass.text = attractionComponent.Mass.ToString("0.#e+0");
   }
   
}