using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AttractionComponent : MonoBehaviour
{

    private float _gravityConstant = (float) (6.67f * Math.Pow(10, -9));

    [SerializeField] 
    private float _pullRadius;
    public bool Active { get; set; }

    public float PullRadius
    {
        get => _pullRadius;
    }

    // Start is called before the first frame update
    void Start()
    {
        Active = true;
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
        float force = _gravityConstant * mass * GetComponent<Rigidbody>().mass / distanceSq;

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