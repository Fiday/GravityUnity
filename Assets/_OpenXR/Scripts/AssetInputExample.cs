using UnityEngine;
using UnityEngine.InputSystem;

public class AssetInputExample : MonoBehaviour
{
    public bool printStuff = false;
    public InputActionReference testReference = null;
    public InputActionReference spawnNewGravityObject = null;
    public GameObject gravityObjectPrefab;

    private void Start()
    {
        testReference.action.started += DoPressedThing;
        testReference.action.performed += DoChangeThing;
        testReference.action.canceled += DoReleasedThing;
        spawnNewGravityObject.action.canceled += SpawnNew;
    }

    private void OnEnable()
    {
        testReference.asset.Enable();
    }

    private void OnDisable()
    {
        testReference.asset.Disable();
    }

    private void OnDestroy()
    {
        testReference.action.started -= DoPressedThing;
        testReference.action.performed -= DoChangeThing;
        testReference.action.canceled -= DoReleasedThing;
        spawnNewGravityObject.action.canceled -= SpawnNew;
    }

    private void DoPressedThing(InputAction.CallbackContext context)
    {
        if (printStuff)
            print("Pressed");
    }

    private void DoChangeThing(InputAction.CallbackContext context)
    {
        if (printStuff)
            print(context.ReadValue<float>());
    }

    private void DoReleasedThing(InputAction.CallbackContext context)
    {
        if (printStuff)
            print("Released");
    }

    private void SpawnNew(InputAction.CallbackContext context)
    {
        GameObject gravityObject = Instantiate(gravityObjectPrefab, transform);
        gravityObject.transform.position = new Vector3(1, 1, -3);
    }
}