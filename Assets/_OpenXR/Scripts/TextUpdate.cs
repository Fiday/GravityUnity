using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TextUpdate : MonoBehaviour
{
    private TextMeshPro textmeshMass;

    private AttractionComponent attractionComponent;
    
    void Start()
    {
        textmeshMass = GameObject.Find("ValueMass").GetComponent<TextMeshPro>();
        attractionComponent = GameObject.FindWithTag("Blackhole").GetComponent<AttractionComponent>();
        textmeshMass.text = attractionComponent.Mass.ToString("0.#e+0");
    }

    public void UpdateTextMesh()
   {
       textmeshMass.text = attractionComponent.Mass.ToString("0.#e+0");
   }
}