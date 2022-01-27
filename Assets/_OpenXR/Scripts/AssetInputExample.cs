using _OpenXR.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class AssetInputExample : MonoBehaviour
{
    public InputActionReference spawnNewGravityObject;
    public InputActionReference spawnNewWithVelocityGravityObject;
    public GameObject planetPrefab;
    public Vector3 spawnOffset = new Vector3(0.2f, -0.4f, 0.3f);

    private void Start()
    {
        spawnNewGravityObject.action.canceled += SpawnNewGravityObject;
        spawnNewWithVelocityGravityObject.action.canceled += SpawnNewWithVelocityGravityObject;
    }

    private void OnDestroy()
    {
        spawnNewGravityObject.action.canceled -= SpawnNewGravityObject;
        spawnNewWithVelocityGravityObject.action.canceled -= SpawnNewWithVelocityGravityObject;
    }

    private void SpawnNewGravityObject(InputAction.CallbackContext context)
    {
        GameObject gravityObject = Instantiate(planetPrefab, transform);
        gravityObject.transform.position = GameObject.Find("Main Camera").transform.position + spawnOffset ;
    }

 

    private void SpawnNewWithVelocityGravityObject(InputAction.CallbackContext context)
    {
        GameObject gravityObject = Instantiate(planetPrefab, transform);
        gravityObject.transform.position = new Vector3(-1.5f, 1.5f, 0f);
        GameObject blackhole = GameObject.FindWithTag("Blackhole");
        float distance = Vector3.Distance(blackhole.transform.position, gravityObject.transform.position);
        gravityObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0,
            Gravity.OrbitVelocity(blackhole.GetComponent<AttractionComponent>().Mass, distance));
    }
}