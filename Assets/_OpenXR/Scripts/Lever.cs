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
            Debug.Log(true);
        }

        if (!state && _lastSwitch)
        {
            _lastSwitch = false;
            offEvent?.Invoke();
            Debug.Log(false);

        }
    }


    /*void CheckHingeValue()
    {
        if (Math.Abs(ValueToAngle () - transform.rotation.eulerAngles.x) > 0.01) {
            float valueToAngle = Mathf.Round (ValueToAngle ());
            float currentAngle = Mathf.Round(transform.rotation.eulerAngles.x);
            Debug.Log ("Setting value to angle value = " + valueToAngle + " cur angle " + currentAngle);
            SetAngleToValue ();
        }
    }*/
    /*
   void OnEnable()
   {
       // Take the current angle of the lever and set our slider value.
       SetAngleToValue ();
   }*/

    // <summary>
    // Sets the angle of the lever to the slider value (
    // </summary>

    /*void SetAngleToValue()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.x = ValueToAngle ();
        Quaternion rot = transform.rotation;
        rot.eulerAngles = rotation;

        transform.rotation = rot;
    }*/

    // <summary>
    // Returns the angle of the lever as a value of the slider
    // </summary>
    // <returns>The to value.</returns>
    float AngleToValue()
    {
        float value = transform.rotation.eulerAngles.x > 180
            ? transform.rotation.eulerAngles.x - 360
            : transform.rotation.eulerAngles.x;
        //Debug.Log ("value = " + value);
        value = Mathf.Clamp(value, Min, Max);
        value += Min * Mathf.Sign(Min);
        return value / (Max + Min * Mathf.Sign(Min));
    }

    /// <summary>
    /// Converts the slider value to the lever angle based on the min and max hinge values
    /// </summary>
    /// <returns>The to angle.</returns>
    float ValueToAngle(float value)
    {
        float angle = (((Max + Min * Mathf.Sign(Min)) * value) - Min * Mathf.Sign(Min) + 360);
        angle = (angle >= 360) ? angle - 360 : angle;
        return angle;
    }
}