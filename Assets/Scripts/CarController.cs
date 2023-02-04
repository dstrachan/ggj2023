using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [SerializeField] private float driftFactor = 0.5f;
    [SerializeField] private float accelerationFactor = 15f;
    [SerializeField] private float turnFactor = 3.5f;
    [SerializeField] private float maxSpeed = 10f;

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
    }

    private void ApplyEngineForce()
    {
        _rigidbody.drag = _accelerationInput == 0 ? Mathf.Lerp(_rigidbody.drag, 3f, Time.fixedDeltaTime * 3) : 0;

        var velocityVsUp = Vector2.Dot(transform.up, _rigidbody.velocity);

        if (velocityVsUp > maxSpeed && _accelerationInput > 0) return;
        if (velocityVsUp < -maxSpeed * 0.5f && _accelerationInput < 0) return;
        if (_rigidbody.velocity.sqrMagnitude > maxSpeed * maxSpeed && _accelerationInput > 0) return;

        var engineForceVector = transform.up * (_accelerationInput * accelerationFactor);
        _rigidbody.AddForce(engineForceVector, ForceMode2D.Force);
    }

    private void ApplySteering()
    {
        var minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(_rigidbody.velocity.magnitude / 8);
        _rotationAngle -= _steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;
        _rigidbody.MoveRotation(_rotationAngle);
    }

    private void KillOrthogonalVelocity()
    {
        var forwardVelocity = Vector2.up * Vector2.Dot(_rigidbody.velocity, Vector2.up);
        var rightVelocity = Vector2.right * Vector2.Dot(_rigidbody.velocity, Vector2.right);
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