using UnityEngine;

public class ForkPickup : MonoBehaviour
{
    Transform carriedPallet;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Pallet")) return;
        if (carriedPallet != null) return;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null) return;

        // Attach pallet
        rb.isKinematic = true;
        other.transform.SetParent(transform);
        carriedPallet = other.transform;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform != carriedPallet) return;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        other.transform.SetParent(null);
        carriedPallet = null;
    }
}
