using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    private float _previousPullRadius = 0;

    private float _gravityConstant = (float)(6.67f * Math.Pow(10, -10));

    [SerializeField]
    private float _mass = 10000000000000000;

    [SerializeField]
    private float _pullRadius = 10f;

    public bool Active { get; set; }

    public float Mass { get => _mass; set => _mass = value; }

    public float PullRadius { get => _pullRadius; set => _pullRadius = value; }

    // Start is called before the first frame update
    void Start()
    {
        Active = true;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (distance > PullRadius)
            return Vector3.zero;
        float distanceSq = distance * distance;

        //calculate gravitational force (F=G*m1*m2/r^2)
        float force = _gravityConstant * mass * Mass / distanceSq;

        Vector3 heading = (transform.position - position);
        //scale force by weight
        Vector3 forceWithDirection = force * (heading / heading.magnitude);

        return forceWithDirection;
    }

    private void UpdatePullRadius()
    {
        var localScale = gameObject.transform.localScale;
        transform.GetChild(0).transform.localScale = new Vector3(PullRadius, PullRadius, PullRadius) * 2;
    }
}