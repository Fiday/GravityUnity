using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class Lever : MonoBehaviour
{
    private float Min = 45;

    private float Max = 135;

    [SerializeField] private UnityEvent onEvent;

    [SerializeField] private UnityEvent offEvent;

    private float triggerRotation = 110f;
    private bool _lastSwitch = false;

    public bool GetCurrentState()
    {
        return ValueToAngle(transform.rotation.x) >= triggerRotation;
    }

    public void SetCurrentState(bool state)
    {
        if (state)
        {
            transform.eulerAngles = new Vector3(45f, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(-45f, 0, 0);
        }
    }


    private void Update()
    {
        var state = GetCurrentState();
        if (state && !_lastSwitch)
        {
            _lastSwitch = true;
            onEvent?.Invoke();
        }

        if (!state && _lastSwitch)
        {
            _lastSwitch = false;
            offEvent?.Invoke();
        }
    }

    float AngleToValue()
    {
        float value = transform.rotation.eulerAngles.x > 180
            ? transform.rotation.eulerAngles.x - 360
            : transform.rotation.eulerAngles.x;
        
        value = Mathf.Clamp(value, Min, Max);
        value += Min * Mathf.Sign(Min);
        return value / (Max + Min * Mathf.Sign(Min));
    }

    float ValueToAngle(float value)
    {
        float angle = (((Max + Min * Mathf.Sign(Min)) * value) - Min * Mathf.Sign(Min) + 360);
        angle = (angle >= 360) ? angle - 360 : angle;
        return angle;
    }
}