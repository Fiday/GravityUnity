using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GravityObject : XRGrabInteractable
{
    private float _currentWeight;
    private float _currentSize;
    private GameObject blackHole;
    private TrailRenderer _trailRenderer;
    
    [SerializeField] private float _minSize = 0.03f;

    [SerializeField] private float _maxSize = 0.6f;

    [SerializeField] private float _minWeight = 0.1f;

    [SerializeField] private float _maxWeight = 150f;

    [SerializeField] private Vector3 _spawnPoint = new (1, 1, -3);
    public float MinSize
    {
        get => _minSize;
    }

    public float MaxSize
    {
        get => _maxSize;
    }

    public float MinWeight
    {
        get => _minWeight;
    }

    public float MaxWeight
    {
        get => _maxWeight;
    }

    public Vector3 SpawnPoint { get => _spawnPoint; }

    public InputActionReference resetReference;
    public InputActionReference ballSizeReference;
    public InputActionReference ballWeightReference;

    protected override void Awake()
    {
        base.Awake();

        resetReference.action.canceled += ResetPos;
        ballSizeReference.action.performed += ChangeBallSize;
        ballWeightReference.action.performed += ChangeBallWeight;
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

    }

    private void ResetPos(InputAction.CallbackContext context)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = _spawnPoint;
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
        var temp = transform.localScale.x;

        temp *= 1 + value / 10f;
        var scale = Mathf.Clamp(temp, MinSize, MaxSize);

        transform.localScale = new Vector3(scale, scale, scale);
        _trailRenderer.startWidth = scale;
        _currentSize = 0f;
    }

    private void ApplyWeightScale(float value)
    {
        if (value == 0f) return;
        Rigidbody ri = GetComponent<Rigidbody>();
        var temp = ri.mass;

        temp *= 1 + value / 10f;
        ri.mass = Mathf.Clamp(temp, MinWeight, MaxWeight);
        _currentWeight = 0f;
        transform.GetChild(0).GetComponent<TextMeshPro>().text = ri.mass.ToString("0.##");
    }

    protected override void SetupRigidbodyGrab(Rigidbody rigidbody)
    {
        Debug.Log($"Rotation: {rigidbody.rotation}");
        base.SetupRigidbodyGrab(rigidbody);
        transform.localRotation = new Quaternion(90, 70, 0, 1);
    }
}