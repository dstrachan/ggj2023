using System;
using UnityEngine;

public class CarEngine : MonoBehaviour
{

    [SerializeField] private AudioSource engineSound;

    
    private Rigidbody2D _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        
    
    }
}