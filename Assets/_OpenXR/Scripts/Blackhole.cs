using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    public float GravityConstant { get; set; }
    
    public bool Active { get; set; }
    
    public float PullRadius { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        Active = true;
        GravityConstant =  (float) (6.67 * Math.Pow(10, -11));
        PullRadius = transform.GetChild(0).transform.localScale.x/2;
    }

    // Update is called once per frame
    void Update()
    {
    }
}