using UnityEngine;

public class ForkliftController : MonoBehaviour
{
    [Header("Movement")]
    public float motorForce = 8000f;
    public float maxSpeed = 6f;
    public float brakeForce = 12000f;

    [Header("Steering")]
    public float maxSteerAngle = 25f;
    public float steeringTorque = 2500f;

    [Header("Stability")]
    public float downForce = 5000f;

    Rigidbody rb;
    float steerInput;
    float moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.4f, -0.2f);
    }
    void FixedUpdate()
    {
        GetInput();
        ApllyDownForce();
        HandleMovement();
        HandleSteering();
        LimitSpeed();
        


    }

    void GetInput()
    {
        steerInput = Input.GetAxis("Horizontal");
        moveInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(moveInput) > 0.01f || Mathf.Abs(steerInput) > 0.01f)
        {
            Debug.Log($"Move: {moveInput}, Steer: {steerInput}");
        }
    }
    void HandleMovement()
    {
        Vector3 force = transform.forward * moveInput * motorForce * Time.fixedDeltaTime;

        if(rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(force, ForceMode.Acceleration);
        }

        //braking
        if (Input.GetKey(KeyCode.Space))
        {
            if (rb.linearVelocity.magnitude > 0.1f)
            {
                rb.AddForce(-rb.linearVelocity.normalized * brakeForce * Time.fixedDeltaTime, ForceMode.Force);
            }

        }
    }

    void HandleSteering()
    {
        if (Mathf.Abs(steerInput) < 0.1f) return;
        
        float turnStength = steerInput * steeringTorque * Time.fixedDeltaTime;
        rb.AddTorque(Vector3.up*turnStength, ForceMode.Force);
    }
    void LimitSpeed()
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }
    void ApllyDownForce()
    {
        rb.AddForce(Vector3.down*downForce*Time.fixedDeltaTime);
            
    }
}
