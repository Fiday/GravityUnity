using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Lever : MonoBehaviour
{
    [Tooltip("Minimum angle in degrees, get's updated by and updates the joint if there is one")]
    public float Min = 0;

    [Tooltip("Maximum angle in degrees, get's updated by and updates the joint if there is one")]
    public float Max = 0;

    private void Update()
    {
        /*Debug.Log(
            transform.GetChild(0).transform.position);*/
    }


    /*void CheckHingeValue()
    {
        if (Mathf.Round(ValueToAngle ()) != Mathf.Round(transform.rotation.eulerAngles.x)) {
            float valueToAngle = Mathf.Round (ValueToAngle ());
            float currentAngle = Mathf.Round(transform.rotation.eulerAngles.x);
            Debug.Log ("Setting value to angle value = " + valueToAngle + " cur angle " + currentAngle);
            SetAngleToValue ();
        }
    }
    
    void OnEnable()
    {
        // Take the current angle of the lever and set our slider value.
        SetAngleToValue ();
    }

    /// <summary>
    /// Sets the angle of the lever to the slider value (
    /// </summary>
    void SetAngleToValue()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.x = ValueToAngle ();
        Quaternion rot = transform.rotation;
        rot.eulerAngles = rotation;

        transform.rotation = rot;
    }

    /// <summary>
    /// Returns the angle of the lever as a value of the slider
    /// </summary>
    /// <returns>The to value.</returns>
    float AngleToValue()
    {
        float value = transform.rotation.eulerAngles.x > 180 ? transform.rotation.eulerAngles.x - 360 : transform.rotation.eulerAngles.x;
        //Debug.Log ("value = " + value);
        value = Mathf.Clamp(value, Min, Max);
        value += Min * Mathf.Sign (Min);
        return value / (Max + Min * Mathf.Sign (Min));
    }

    /// <summary>
    /// Converts the slider value to the lever angle based on the min and max hinge values
    /// </summary>
    /// <returns>The to angle.</returns>
    float ValueToAngle()
    {
        float angle = (((Max + Min * Mathf.Sign (Min)) * Value) - Min * Mathf.Sign (Min) + 360);
        angle = (angle >= 360) ? angle - 360: angle;
        return angle;
    }
    */
}