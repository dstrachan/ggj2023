using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDrift : MonoBehaviour
{

    private Rigidbody2D _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var right = transform.right;
        Vector2 rightVelocity = right * Vector2.Dot(_rigidbody.velocity, right);
        
        print(rightVelocity.magnitude);
    }
}
