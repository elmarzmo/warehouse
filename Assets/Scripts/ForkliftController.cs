using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ForkliftController : MonoBehaviour
{
    public float motorForce = 6000f;
    public float maxSpeed = 6f;
    public float braking = 8f;
    public float steeringSpeed = 90f; // degrees/sec
    public float downForce = 3000f;

    Rigidbody rb;
    float forwardInput;
    float steerInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        ApplyDownForce();
        HandleMovement();
        HandleSteering();
        LimitSpeed();

        if (rb.velocity.y > 1f)
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    }

    void HandleMovement()
    {
        if (Mathf.Abs(forwardInput) < 0.01f)
        {
            // natural braking
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, braking * Time.fixedDeltaTime);
            return;
        }

        rb.AddForce(transform.forward * forwardInput * motorForce, ForceMode.Force);
    }

    void HandleSteering()
    {
        if (Mathf.Abs(forwardInput) < 0.1f) return;

        float speedFactor = Mathf.Clamp01(rb.velocity.magnitude / maxSpeed);
        float turnAmount = steerInput * steeringSpeed * speedFactor * Time.fixedDeltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turnAmount, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    void LimitSpeed()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVelocity.magnitude > maxSpeed)
        {
            Vector3 limited = flatVelocity.normalized * maxSpeed;
            rb.velocity = new Vector3(limited.x, rb.velocity.y, limited.z);
        }
    }

    void ApplyDownForce()
    {
        
        
        rb.AddForce(Vector3.down * downForce, ForceMode.Force);
        
    }
}
