using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsTest : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.WakeUp();                 // ?? FORCE WAKE
        rb.AddForce(Vector3.forward * 1f, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        rb.WakeUp();                 // ?? KEEP IT AWAKE
        rb.AddForce(Vector3.forward * 2000f, ForceMode.Force);
    }
}