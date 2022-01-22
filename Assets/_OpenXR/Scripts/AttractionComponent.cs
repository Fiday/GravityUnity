using System;
using System.Linq;
using _OpenXR.Scripts;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AttractionComponent : MonoBehaviour
{
    
    [SerializeField] 
    private float _pullRadius;
    public float PullRadius
    {
        get => _pullRadius;
    }
    
    [SerializeField] 
    private float _mass;
    public float Mass
    {
        get => _mass;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        AddForceToAffectedObjects();
    }

    private Vector3 CalculateGravityPull(Vector3 position, float mass)
    {
        //get Distance
        var currPosition = transform.position;
        
        float distance = Vector3.Distance(position, currPosition);
        float distanceSq = distance * distance;

        //calculate gravitational force (F=G*m1*m2/r^2)
        float force = Gravity.GravityConstant * mass * (Mass / distanceSq);

        Vector3 heading = currPosition - position;
        //scale force by weight
        Vector3 forceWithDirection = force * (heading / heading.magnitude);

        return forceWithDirection * Time.deltaTime;
    }

    private void AddForceToAffectedObjects()
    {
        var colliders = Physics.OverlapSphere(transform.position, PullRadius)
            .Where(c => c.gameObject != gameObject && c.gameObject.tag.Equals("Gravity"));

        foreach (Collider col in colliders)
        {
            var rigidBody = col.gameObject.GetComponent<Rigidbody>();
            rigidBody.AddForce(CalculateGravityPull(rigidBody.transform.position, rigidBody.mass),
                ForceMode.Impulse);
        }
    }
}