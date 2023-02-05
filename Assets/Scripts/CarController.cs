using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [SerializeField] private float driftFactor = 0.2f;
    [SerializeField] private float turnFactor = 3.5f;

    [SerializeField] private float accelerationFactor = 7.5f;
    [SerializeField] private float maxSpeed = 10f;

    [SerializeField] private float reverseFactor = 3.5f;
    [SerializeField] private float maxReverse = 5f;

    [SerializeField] private float turnBuildupThreshold = 2.0f;

    [SerializeField] private AudioSource engineSound;

    
    private float _accelerationInput;
    private float _steeringInput;

    private float _rotationAngle;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();
        KillOrthogonalVelocity();
        ApplySteering();

        if (_accelerationInput > 0)
        {
            engineSound.pitch = 1 * (1 + _rigidbody.velocity.magnitude/maxSpeed);
        }
    }

    private void ApplyEngineForce()
    {
        var velocityVsUp = Vector2.Dot(transform.up, _rigidbody.velocity);

        // If not accelerating, start dragging
        if ((_accelerationInput <= 0 && velocityVsUp > 0) || (_accelerationInput >= 0 && velocityVsUp < 0))
        {
            _rigidbody.drag = Mathf.Lerp(_rigidbody.drag, 1f, Time.fixedDeltaTime * 3);
        }
        else
        {
            _rigidbody.drag = 0;
        }

        if (velocityVsUp > maxSpeed && _accelerationInput > 0) return;
        if (velocityVsUp < -maxReverse && _accelerationInput < 0) return;
        if (_rigidbody.velocity.sqrMagnitude > maxSpeed * maxSpeed && _accelerationInput > 0) return;

        var engineForceVector = _accelerationInput > 0 ? transform.up * (_accelerationInput * accelerationFactor) : transform.up * (_accelerationInput * reverseFactor);
        _rigidbody.AddForce(engineForceVector, ForceMode2D.Force);
    }

    private void ApplySteering()
    {
        var velocityVsUp = Vector2.Dot(transform.up, _rigidbody.velocity);
        if(Mathf.Abs(velocityVsUp) < turnBuildupThreshold)
        {
            _rotationAngle -= _steeringInput * turnFactor * (Mathf.Abs(velocityVsUp) / turnBuildupThreshold);
        }
        else
        {
            _rotationAngle -= _steeringInput * turnFactor;
        }

        _rigidbody.MoveRotation(_rotationAngle);
    }

    private void KillOrthogonalVelocity()
    {
        var up = transform.up;
        Vector2 forwardVelocity = up * Vector2.Dot(_rigidbody.velocity, up);
        var right = transform.right;
        Vector2 rightVelocity = right * Vector2.Dot(_rigidbody.velocity, right);

        var velocityVsUp = Vector2.Dot(transform.up, _rigidbody.velocity);

        _rigidbody.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    private void OnAccelerate(InputValue value)
    {
        _accelerationInput = value.Get<float>();
    }

    private void OnSteer(InputValue value)
    {
        _steeringInput = value.Get<float>();
    }
}