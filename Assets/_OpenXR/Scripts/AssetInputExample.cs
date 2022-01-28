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
        GameObject blackhole = GameObject.FindWithTag("Blackhole");
        
        var position = blackhole.transform.position;
        gravityObject.transform.position = position +  new Vector3(-2f,0f, 0f);
        float distance = Vector3.Distance(position, gravityObject.transform.position);
        gravityObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0,
            Gravity.OrbitVelocity(blackhole.GetComponent<AttractionComponent>().Mass, distance));
    }
}