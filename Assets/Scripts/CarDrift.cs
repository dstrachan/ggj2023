using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CarDrift : MonoBehaviour
{

    public VisualEffect _dustE; 
    public VisualEffect _dustW; 
    public TrailRenderer _trailE; 
    public TrailRenderer _trailW; 
    
    private Rigidbody2D _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _trailE.emitting = false;
        _trailW.emitting = false;
    }

    private void FixedUpdate()
    {
        var right = transform.right;
        Vector2 rightVelocity = right * Vector2.Dot(_rigidbody.velocity, right);
        
        if (rightVelocity.magnitude > 5)
        {
            _dustE.Play();
            _dustW.Play();
            _trailE.emitting = true;
            _trailW.emitting = true;
        }
        else
        {
            
            _dustE.Stop();
            _dustW.Stop();
            _trailE.emitting = false;
            _trailW.emitting = false;
        }
    }
}
