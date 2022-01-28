using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SliderScript : XRGrabInteractable
{
    private float _range = 1;


    // Start is called before the first frame update
    void Start()
    {
    }

    public float GetCurrentValue()
    {
        var x = transform.localPosition.x;

        return x / 2 + 0.5f;
    }

    public void SetCurrentValue(float x)
    {
        transform.localPosition = new Vector3((x - 0.5f) * 2, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.localPosition;
        if (pos.x > _range)
        {
            SetCurrentValue(_range);
        }
        else if (pos.x < -_range)
        {
            SetCurrentValue(0);
        }

        if (isSelected)
        {
            //GetComponent<XRGrabInteractable>().isSelected
            var hand = GameObject.Find("RightHand Controller");
            Vector3 newPos = new Vector3(hand.transform.position.x, 0, 0);
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime);
        }


        Debug.Log(GetCurrentValue());
    }
}