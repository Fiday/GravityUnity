using System;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    private float _currPullRadius;

    // Update is called once per frame
    void Update()
    {
        var scale = GetComponent<AttractionComponent>().PullRadius*2;
        if (Math.Abs(scale - _currPullRadius) > 0.01)
        {
            transform.GetChild(0).localScale = new Vector3(scale, scale, scale);
            _currPullRadius = scale;
        }
    }
}
