using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.XR.Interaction.Toolkit;

public class GravityObject : XRGrabInteractable
{
    private GameObject blackHole;

    private Vector3 _originalScale = new Vector3(0.2f, 0.2f, 0.2f);

    // Start is called before the first frame update
    void Start()
    {
        blackHole = GameObject.FindGameObjectWithTag("BlackHole");
    }

    private void Update()
    {
        if (IsControllerActionBased(out var controller))
        {
            // Now that we know we have the right controller, get the value of the activate (trigger-pull)
            var activateValue = GetActionValue(controller.activateAction);

            Debug.Log($"Presseforce = {activateValue}");

            // Apply that value to the object
            if (Math.Abs(activateValue - 1) < 0.01)
            {
                ResetPos();
            }
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (blackHole.GetComponent<Blackhole>().Active)
        {
            var blackHolePos = blackHole.transform.position;
            var blackHoleObject = blackHole.GetComponent<Blackhole>();
            var current = transform.position;
            Vector3 gravityVector = blackHolePos - current;

            float gravityDistance = Vector3.Distance(blackHolePos, current);
            if (gravityDistance < blackHoleObject.PullRadius)
            {
                GetComponent<Rigidbody>().AddForce(5.0f * gravityVector);
            }
            /*Debug.Log($"Current: {current}");
            Vector3 gravityVector = blackHolePos - current;
            float gravityDistance = Vector3.Distance(blackHolePos, current);
            Debug.Log($"Distance: {gravityDistance}");
            Debug.Log($"PullRange: {blackHoleObject.PullRadius}");
            if (gravityDistance < blackHoleObject.PullRadius)
            {
                Vector3 gravityStrength = Vector3.zero;
                gravityStrength.x = blackHoleObject.GravityConstant / Mathf.Pow(gravityDistance, 2);
                gravityStrength.z = blackHoleObject.GravityConstant / Mathf.Pow(gravityDistance, 2);
                gravityStrength.y = blackHoleObject.GravityConstant / Mathf.Pow(gravityDistance, 2);
                Debug.Log($"PullRange: {blackHoleObject.GravityConstant}");

                force = Vector3.Scale(gravityStrength, gravityVector);
                Debug.Log("HALLI");
            }*/
        }
    }


    private MeshRenderer _meshRenderer = null;


    protected override void Awake()
    {
        base.Awake();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void ResetPos()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = new Vector3(1, 1, -3);
    }

    private bool IsControllerActionBased(out ActionBasedController controller)
    {
        controller = null;

        // Needs to at least by a Base Controller Interactor
        if (selectingInteractor is XRBaseControllerInteractor interactor)
        {
            // Make sure that Controller is Action-Based
            if (interactor.xrController is ActionBasedController actionBasedController)
                controller = actionBasedController;
        }

        // Return a bool so we don't need this null-check else where
        return controller != null;
    }

    private float GetActionValue(InputActionProperty inputAction)
    {
        // Read the float value, this can be a more advanced function with generics
        return inputAction.action.ReadValue<float>();
    }

    private void ApplyScale(float value)
    {
        var newScale = _originalScale + (_originalScale * value);
        transform.localScale = newScale;
    }
}