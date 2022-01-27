using System;
using System.Linq;
using _OpenXR.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttractionComponent : MonoBehaviour
{
    [SerializeField] private bool attracts;

    public bool Attracts
    {
        get => attracts;
        set => attracts = value;
    }
    [SerializeField] private float _pullRadius;

    public float PullRadius
    {
        get => _pullRadius;
    }

    [SerializeField] private float _mass;

    public float Mass
    {
        get => _mass;
        set => _mass = value;
    }

    private void Start()
    {
        if (TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.mass = Mass;
        }
    }


    void Update()
    {
        if (attracts)
            AddForceToAffectedObjects();
    }

    private Vector3 CalculateGravityPull(Vector3 position, float mass)
    {
        //get Distance
        var currPosition = transform.position;
        float distance = Vector3.Distance(position, currPosition);
        float distanceSq = distance * distance;

        //calculate gravitational force (F=G*m1*m2/r^2)
        float force = Gravity.GravityConstant * (Mass * mass  / distanceSq);

        Vector3 heading = currPosition - position;
        //scale force by weight
        Vector3 forceWithDirection = force * (heading / heading.magnitude);

        return forceWithDirection * Time.deltaTime;
    }

    private void AddForceToAffectedObjects()
    {
        var colliders = Physics.OverlapSphere(transform.position, _pullRadius)
            .Where(c => c.gameObject != gameObject && c.gameObject.tag.Equals("Gravity"));

        foreach (Collider col in colliders)
        {
            var rigidBody = col.gameObject.GetComponent<Rigidbody>();
            var mass = col.gameObject.GetComponent<AttractionComponent>().Mass;
            rigidBody.AddForce(CalculateGravityPull(rigidBody.transform.position, mass),
                ForceMode.Impulse);
        }
    }
}