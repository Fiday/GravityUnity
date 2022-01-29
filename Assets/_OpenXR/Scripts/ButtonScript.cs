using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private UnityEvent pressEvent;
    [SerializeField] private UnityEvent holdEvent;
    [SerializeField] private UnityEvent releaseEvent;
    private bool _pressed;
    private XRGrabInteractable _xrGrabInteractable;

    // Start is called before the first frame update
    void Start()
    {
        _xrGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_xrGrabInteractable.isSelected && !_pressed)
        {
            _pressed = true;
            pressEvent.Invoke();
        }
        if (_xrGrabInteractable.isSelected)
        {
            holdEvent.Invoke();
        }
        else if (!_xrGrabInteractable.isSelected && _pressed)
        {
            _pressed = false;
            releaseEvent.Invoke();
        }
    }
}