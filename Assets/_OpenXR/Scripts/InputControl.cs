using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class InputControl : XRGrabInteractable
{
    private float _currentWeight;
    private float _currentSize;

    // Start is called before the first frame update
    [SerializeField] private float _minSize;

    [SerializeField] private float _maxSize;

    [SerializeField] private float _minWeight;

    [SerializeField] private float _maxWeight;
    
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
    
    public InputActionReference ballSizeReference;
    public InputActionReference ballWeightReference;

    protected override void Awake()
    {
        base.Awake();

        ballSizeReference.action.performed += ChangeBallSize;
        ballWeightReference.action.performed += ChangeBallWeight;
    }
    
    protected override void OnDestroy()
    {
        ballSizeReference.action.performed -= ChangeBallSize;
        ballWeightReference.action.performed -= ChangeBallWeight;

        base.OnDestroy();
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

                gameObject.GetComponent<InputCallback>().ObjectChanged();
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
    }
}
