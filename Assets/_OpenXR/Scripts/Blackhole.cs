using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    private float _previousPullRadius = 0;

    private float _gravityConstant = (float) (6.67f * Math.Pow(10, -9));

    [SerializeField] private float _mass;

    [SerializeField] private float _pullRadius;
    public bool Active { get; set; }

    public float Mass
    {
        get => _mass;
    }

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
        if (Math.Abs(_pullRadius - _previousPullRadius) > 0.01)
        {
            UpdatePullRadius();
        }

        _previousPullRadius = _pullRadius;
    }

    public Vector3 CalculateGravityPull(Vector3 position, float mass)
    {
        //get Distance
        float distance = Vector3.Distance(position, transform.position);
        // if (distance > PullRadius)
        //     return Vector3.zero;
        float distanceSq = distance * distance;

        //calculate gravitational force (F=G*m1*m2/r^2)
        float force = _gravityConstant * mass * Mass / distanceSq;

        Vector3 heading = transform.position - position;
        //scale force by weight
        Vector3 forceWithDirection = force * (heading / heading.magnitude);

        return forceWithDirection * Time.deltaTime;
    }

    private void UpdatePullRadius()
    {
        transform.GetChild(0).transform.localScale = new Vector3(PullRadius, PullRadius, PullRadius) * 2;
    }

    private void AddForceToAffectedObjects()
    {
        var colliders = Physics.OverlapSphere(transform.position, PullRadius)
            .Where(c => c.gameObject.tag.Equals("Gravity"));

        foreach (Collider col in colliders)
        {
            var rigidBody = col.gameObject.GetComponent<Rigidbody>();
            rigidBody.AddForce(CalculateGravityPull(rigidBody.transform.position, rigidBody.mass),
                ForceMode.Impulse);
        }
    }
}