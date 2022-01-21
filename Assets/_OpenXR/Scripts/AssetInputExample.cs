using UnityEngine;
using UnityEngine.InputSystem;

public class AssetInputExample : MonoBehaviour
{
    public InputActionReference spawnNewGravityObject;
    public InputActionReference spawnNewPullyObject;
    public GameObject gravityObjectPrefab;
    public GameObject pullyObjectPrefab;

    private void Start()
    {
        spawnNewGravityObject.action.canceled += SpawnNewGravityObject;
        spawnNewPullyObject.action.canceled += SpawnNewPullyObject;
    }

    private void OnDestroy()
    {
        spawnNewGravityObject.action.canceled -= SpawnNewGravityObject;
        spawnNewPullyObject.action.canceled -= SpawnNewPullyObject;
    }

    private void SpawnNewGravityObject(InputAction.CallbackContext context)
    {
        GameObject gravityObject = Instantiate(gravityObjectPrefab, transform);
        gravityObject.transform.position = new Vector3(1, 1, -3);
    }
    
    private void SpawnNewPullyObject(InputAction.CallbackContext context)
    {
        GameObject pullyObject = Instantiate(pullyObjectPrefab, transform);
        pullyObject.transform.position = new Vector3(1, 1, -3);
    }
}