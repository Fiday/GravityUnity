using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(1).GetComponent<TextMeshPro>().text = GameObject.FindWithTag("Blackhole").GetComponent<AttractionComponent>().Mass.ToString("0.#e+0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
