using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GravityObject : XRGrabInteractable
{
    private float _currentWeight = 0f;
    private float _currentSize = 0f;
    private Vector3 _originalScale = new Vector3(0.2f, 0.2f, 0.2f);
    private GameObject blackHole;

    [SerializeField] private float _minSize = 0.03f;

    [SerializeField] private float _maxSize = 0.6f;

    [SerializeField] private float _minWeight = 0.1f;

    [SerializeField] private float _maxWeight = 150f;

    public float MinSize
    {
        get => _minSize;
        set => _minSize = value;
    }

    public float MaxSize
    {
        get => _maxSize;
        set => _maxSize = value;
    }

    public float MinWeight
    {
        get => _minWeight;
        set => _minWeight = value;
    }

    public float MaxWeight
    {
        get => _maxWeight;
        set => _maxWeight = value;
    }

    public InputActionReference resetReference = null;
    public InputActionReference ballSizeReference = null;
    public InputActionReference ballWeightReference = null;

    private MeshRenderer _meshRenderer = null;
    private TrailRenderer _trailRenderer = null;

    protected override void Awake()
    {
        base.Awake();

        resetReference.action.canceled += ResetPos;
        ballSizeReference.action.performed += ChangeBallSize;
        ballWeightReference.action.performed += ChangeBallWeight;
        blackHole = GameObject.FindGameObjectWithTag("BlackHole");
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _trailRenderer = GetComponentInChildren<TrailRenderer>();
        _trailRenderer.startWidth = transform.localScale.x;
        
        transform.GetChild(0).GetComponent<TextMeshPro>().text = GetComponent<Rigidbody>().mass.ToString("0.##");

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
        /*var blackHoleObject = blackHole.GetComponent<Blackhole>();
        if (blackHoleObject.Active)
        {
            var rigidBody = GetComponent<Rigidbody>();
            rigidBody.AddForce(blackHoleObject.CalculateGravityPull(transform.position, rigidBody.mass),
                ForceMode.Impulse);
        }*/
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
                ApplyScale(_currentSize);
                ApplyWeightScale(_currentWeight);
            }
        }
    }


    /*
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
    */


    private void ChangeBallSize(InputAction.CallbackContext context)
    {
        _currentSize = context.ReadValue<float>();

    }

    private void ChangeBallWeight(InputAction.CallbackContext context)
    {
        _currentWeight = context.ReadValue<float>();
    }
    
    private void ApplyScale(float value)
    {
        if (value == 0f) return;
        var localScale = transform.localScale;
        var temp = localScale.x;

        temp *= 1 + value / 10f;
        var scale = Mathf.Clamp(temp, MinSize, MaxSize);

        localScale = new Vector3(scale, scale, scale);
        transform.localScale = localScale;
        _trailRenderer.startWidth = localScale.x;
        _currentSize = 0f;
    }

    private void ApplyWeightScale(float value)
    {
        if (value == 0) return;
        Rigidbody ri = GetComponent<Rigidbody>();
        var temp = ri.mass;

        temp *= 1 + value / 10f;
        var scale = Mathf.Clamp(temp, MinWeight, MaxSize);
        GetComponent<Rigidbody>().mass = scale;
        _currentWeight = 0f;
        transform.GetChild(0).GetComponent<TextMeshPro>().text = scale.ToString("0.##");
    }
}