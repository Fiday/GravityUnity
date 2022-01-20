using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GravityObject : XRGrabInteractable
{
    private GameObject blackHole;

    private Vector3 _originalScale = new Vector3(0.2f, 0.2f, 0.2f);
    public InputActionReference resetReference = null;

    private MeshRenderer _meshRenderer = null;

    protected override void Awake()
    {
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
        var blackHoleObject = blackHole.GetComponent<Blackhole>();
        if (blackHoleObject.Active)
        {
            var rigidBody = GetComponent<Rigidbody>();
            rigidBody.AddForce(blackHoleObject.CalculateGravityPull(transform.position, rigidBody.mass), ForceMode.Impulse);
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

        // Needs to at least be a Base Controller Interactor
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