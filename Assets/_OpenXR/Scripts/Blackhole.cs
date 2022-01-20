using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    public float GravityConstant { get; set; }

    public bool Active { get; set; }

    public float pullRadius = 1.5f;
    private float previousPullRadius = 0;

    // Start is called before the first frame update
    void Start()
    {
        Active = true;
        GravityConstant = (float) (6.67 * Math.Pow(10, -11));
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(pullRadius - previousPullRadius) > 0.01)
        {
            UpdatePullRadius();
        }

        previousPullRadius = pullRadius;
    }

    private void UpdatePullRadius()
    {
        var localScale = gameObject.transform.localScale;
        transform.GetChild(0).transform.localScale = new Vector3(pullRadius, pullRadius, pullRadius) * 2;
    }
}