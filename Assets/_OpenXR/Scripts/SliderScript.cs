using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderScript : MonoBehaviour
{
    private float _range = 1;


    // Start is called before the first frame update
    void Start()
    {
    }

    public float GetCurrentValue()
    {
        var x = transform.GetChild(1).transform.localPosition.x;

        return x / 2 + 0.5f;
    }

    void SetCurrentValue(float x)
    {
        transform.GetChild(1).transform.localPosition = new Vector3((x - 0.5f) * 2, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.GetChild(1).transform.localPosition;
        if (pos.x > _range)
        {
            SetCurrentValue(_range);
        }
        else if (pos.x < -_range)
        {
            SetCurrentValue(0);
        }
        
        
        Debug.Log(GetCurrentValue());
    }
}