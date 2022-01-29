using System;
using _OpenXR.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting;
using UnityEngine.XR.Interaction.Toolkit;


public class InputControl : XRGrabInteractable
{
    private const float SCIENTIFIC_CUTOFF = 100000;
    private const string NON_SCIENTIFIC_FORMAT = "0.#";
    private const string SCIENTIFIC_FORMAT = "0.#e+0";

    private float _currentWeight;
    private float _currentSize;

    private TrailRenderer _trailRenderer;

    [SerializeField] private float _minSize;

    [SerializeField] private float _maxSize;

    [SerializeField] private float _minWeight;

    [SerializeField] private float _maxWeight;

    [SerializeField] private float _outOfBounds = 20;

    [SerializeField] private InputActionReference _ballSizeReference;

    [SerializeField] private InputActionReference _ballWeightReference;

    [SerializeField] private InputActionReference toggleAttraction;

    [SerializeField] private  Material sunMaterial;
    
    [SerializeField] private Material planetMaterial;

    protected override void Awake()
    {
        base.Awake();
        _outOfBounds = GameObject.FindWithTag("Blackhole").GetComponent<AttractionComponent>().PullRadius;
        _ballSizeReference.action.performed += ChangeBallSize;
        _ballWeightReference.action.performed += ChangeBallWeight;
        toggleAttraction.action.canceled += ToggleAttraction;

        _trailRenderer = GetComponent<TrailRenderer>();
        UpdateTrail();
    }

    private void Update()
    {
        if (Vector3.Distance(Vector3.zero, transform.localPosition) > _outOfBounds)
            Destroy(gameObject);
    }

    protected override void OnDestroy()
    {
        _ballSizeReference.action.performed -= ChangeBallSize;
        _ballWeightReference.action.performed -= ChangeBallWeight;
        toggleAttraction.action.canceled -= ToggleAttraction;

        base.OnDestroy();
    }

    private void ToggleAttraction(InputAction.CallbackContext obj)
    {
        if (isSelected)
        {
            var atc = GetComponent<AttractionComponent>();
            atc.Attracts = !atc.Attracts;
            transform.GetChild(1).GetComponent<MeshRenderer>().material =
                atc.Attracts ? sunMaterial : planetMaterial;
        }
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
                //GetComponent<SoundScript>().AddSound();
            }

            var format = GetComponent<AttractionComponent>().Mass > SCIENTIFIC_CUTOFF
                ? SCIENTIFIC_FORMAT
                : NON_SCIENTIFIC_FORMAT;
            GetComponentInChildren<TextMeshPro>().text = GetComponent<AttractionComponent>().Mass.ToString(format);
        }
        else
        {
            GetComponentInChildren<TextMeshPro>().text = String.Empty;
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
        var scale = Mathf.Clamp(temp, _minSize, _maxSize);

        transform.localScale = new Vector3(scale, scale, scale);
        _currentSize = 0f;

        UpdateTrail();
    }

    private void ApplyWeightScale(float value)
    {
        if (value == 0f) return;
        var attractionComp = GetComponent<AttractionComponent>();
        var temp = attractionComp.Mass;

        temp *= 1 + value / 10f;
        attractionComp.Mass = Mathf.Clamp(temp, _minWeight, _maxWeight);
        GetComponent<Rigidbody>().mass = attractionComp.Mass;
        _currentWeight = 0f;
    }

    private void UpdateTrail()
    {
        var localScale = transform.localScale;
        _trailRenderer.startWidth = localScale.x / 5f;
        _trailRenderer.endWidth = localScale.x / 5f;
    }
}