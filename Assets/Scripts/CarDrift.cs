using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CarDrift : MonoBehaviour
{

    public float minDriftSpeedForEffects = 5f;

    public VisualEffect dustE; 
    public VisualEffect dustW; 
    public TrailRenderer trailE; 
    public TrailRenderer trailW;

    private bool vfxPlaying;
    
    public AudioSource driftSound;
    
    private Rigidbody2D _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        trailE.emitting = false;
        trailW.emitting = false;
    }

    private void FixedUpdate()
    {
        var right = transform.right;
        Vector2 rightVelocity = right * Vector2.Dot(_rigidbody.velocity, right);
        
        if (rightVelocity.magnitude > minDriftSpeedForEffects)
        {
            if (!vfxPlaying)
            {
                vfxPlaying = true;
                dustE.Play();
                dustW.Play();
            }

            if (!driftSound.isPlaying)
            {
                driftSound.Play();
            }

            trailE.emitting = true;
            trailW.emitting = true;
        }
        else
        {
            if (driftSound.isPlaying)
            {
                driftSound.Stop();
            }

            if (vfxPlaying)
            {
                dustE.Stop();
                dustW.Stop();
                vfxPlaying = false;
            }

            if (trailE.emitting)
            {
                trailE.emitting = false;
            }

            if (trailW.emitting)
            {
                trailW.emitting = false;
            }
        }
    }
}
