using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GravityObject : XRGrabInteractable
{
    private GameObject blackHole;

    private float minSize = 0.03f;
    private float maxSize = 0.6f;
    private float currentSize = 0f;

    private float minWeight = 0.1f;
    private float maxWeight = 150f;
    private float currentWeight = 0f;

    private Vector3 _originalScale = new Vector3(0.2f, 0.2f, 0.2f);
    public InputActionReference resetReference = null;
    public InputActionReference ballSizeReference = null;
    public InputActionReference ballWeightReference = null;

    private MeshRenderer _meshRenderer = null;

    protected override void Awake()
    {
        base.Awake();

        resetReference.action.canceled += ResetPos;
        ballSizeReference.action.performed += ChangeBallSize;
        ballWeightReference.action.performed += ChangeBallWeight;
        blackHole = GameObject.FindGameObjectWithTag("BlackHole");
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    protected override void OnDestroy()
    {
        resetReference.action.canceled -= ResetPos;
        ballSizeReference.action.performed -= ChangeBallSize;
        ballWeightReference.action.performed -= ChangeBallWeight;


        base.OnDestroy();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
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

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        // If the object is held
        if (isSelected)
        {
            // During Update
            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            {
                ApplyScale(currentSize);
                ApplyWeightScale(currentWeight);
            }
        }
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


    private void ChangeBallSize(InputAction.CallbackContext context)
    {
        //Debug.Log($"Joystick = {context.ReadValue<Vector2>().y}");
        currentSize = context.ReadValue<float>();
        // GetComponent<Rigidbody>().velocity = Vector3.zero;
        // transform.position = new Vector3(1, 1, -3);
    }

    private void ChangeBallWeight(InputAction.CallbackContext context)
    {
        //Debug.Log($"Joystick = {context.ReadValue<Vector2>().y}");
        currentWeight = context.ReadValue<float>();
        // GetComponent<Rigidbody>().velocity = Vector3.zero;
        // transform.position = new Vector3(1, 1, -3);
    }


    private float GetActionValue(InputActionProperty inputAction)
    {
        // Read the float value, this can be a more advanced function with generics
        return inputAction.action.ReadValue<float>();
    }

    private void ApplyScale(float value)
    {
        if (value == 0f) return;
        var temp = transform.localScale.x;

        temp *= 1 + value / 10f;
        var scale = Mathf.Clamp(temp, minSize, maxSize);

        transform.localScale = new Vector3(scale, scale, scale);
        currentSize = 0f;
    }

    private void ApplyWeightScale(float value)
    {
        if (value == 0) return;
        Rigidbody ri = GetComponent<Rigidbody>();
        var temp = ri.mass;

        temp *= 1 + value / 10f;
        var scale = Mathf.Clamp(temp, minWeight, maxWeight);
        GetComponent<Rigidbody>().mass = scale;
        currentWeight = 0f;
    }
}