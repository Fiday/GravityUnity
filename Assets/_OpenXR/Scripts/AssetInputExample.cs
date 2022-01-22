using _OpenXR.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class AssetInputExample : MonoBehaviour
{
    public InputActionReference spawnNewGravityObject;
    public InputActionReference spawnNewPullyObject;
    public InputActionReference spawnNewWithVelocityGravityObject;
    public GameObject gravityObjectPrefab;
    public GameObject pullyObjectPrefab;
    private Vector3 _spawnPoint = new Vector3(1, 1, -3);

    private void Start()
    {
        spawnNewGravityObject.action.canceled += SpawnNewGravityObject;
        spawnNewPullyObject.action.canceled += SpawnNewPullyObject;
        spawnNewWithVelocityGravityObject.action.canceled += SpawnNewWithVelocityGravityObject;
    }

    private void OnDestroy()
    {
        spawnNewGravityObject.action.canceled -= SpawnNewGravityObject;
        spawnNewPullyObject.action.canceled -= SpawnNewPullyObject;
        spawnNewWithVelocityGravityObject.action.canceled -= SpawnNewWithVelocityGravityObject;
    }

    private void SpawnNewGravityObject(InputAction.CallbackContext context)
    {
        GameObject gravityObject = Instantiate(gravityObjectPrefab, transform);
        gravityObject.transform.position = _spawnPoint;
    }

    private void SpawnNewPullyObject(InputAction.CallbackContext context)
    {
        GameObject pullyObject = Instantiate(pullyObjectPrefab, transform);
        pullyObject.transform.position = _spawnPoint;
    }

    private void SpawnNewWithVelocityGravityObject(InputAction.CallbackContext context)
    {
        GameObject gravityObject = Instantiate(gravityObjectPrefab, transform);
        gravityObject.transform.position = new Vector3(-1.5f, 1.5f, 0f);
        GameObject blackhole = GameObject.Find("Blackhole");
        float distance = Vector3.Distance(blackhole.transform.position, gravityObject.transform.position);
        gravityObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0,
            Gravity.OrbitVelocity(blackhole.GetComponent<AttractionComponent>().Mass, distance));
    }
}