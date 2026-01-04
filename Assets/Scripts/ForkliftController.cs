using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ForkliftController : MonoBehaviour
{
    public float acceleration = 4f;
    public float maxSpeed = 6f;
    public float braking = 8f;
    public float steeringSpeed = 90f; // degrees per second

    float currentSpeed;
    Rigidbody rb;
    Vector3 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        float forward = Input.GetAxis("Vertical");
        float steer = Input.GetAxis("Horizontal");

        // Acceleration & braking
        if (Mathf.Abs(forward) > 0.01f)
        {
            currentSpeed += forward * acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, braking * Time.deltaTime);
        }

        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

        // Steering only when moving
        if (Mathf.Abs(currentSpeed) > 0.2f)
        {
            float turn = steer * steeringSpeed * Time.deltaTime;
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turn, 0f));
        }

        movementInput = transform.forward * currentSpeed;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementInput * Time.fixedDeltaTime);
    }
}
