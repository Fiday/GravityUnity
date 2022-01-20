using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GravityObject : XRGrabInteractable
{
    private GameObject blackHole;
    //   private XRIDefaultInputActions _controls;

    private Vector3 _originalScale = new Vector3(0.2f, 0.2f, 0.2f);
    public InputActionReference resetReference = null;

    private MeshRenderer _meshRenderer = null;

    protected override void Awake()
    {
        /*_controls = new XRIDefaultInputActions();
        _controls.XRIRightHand.SetCallbacks(this);*/
        base.Awake();

        resetReference.action.started += ResetPos;
        blackHole = GameObject.FindGameObjectWithTag("BlackHole");
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    protected override void OnDestroy()
    {
        resetReference.action.started -= ResetPos;
        base.OnDestroy();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (IsControllerActionBased(out var controller))
        {
            // Now that we know we have the right controller, get the value of the activate (trigger-pull)
            var activateValue = GetActionValue(controller.activateAction);
        }
    }
    //TODO Watch this
    //https://www.youtube.com/watch?v=jOn0YWoNFVY

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
            Debug.Log(blackHoleObject.pullRadius);
            if (gravityDistance < blackHoleObject.pullRadius)
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
    
    private void ResetPos(InputAction.CallbackContext context)
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