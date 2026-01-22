using UnityEngine;

public class ForkliftPalletHandler : MonoBehaviour
{
    public Transform palletHoldPoint;
    public KeyCode pickupKey = KeyCode.E;
    public KeyCode releaseKey = KeyCode.Q;

    Rigidbody carriedPallet;

    void Update()
    {
        if (Input.GetKeyDown(pickupKey) && carriedPallet == null)
        {
            TryPickup();
        }

        if (Input.GetKeyDown(releaseKey) && carriedPallet != null)
        {
            Release();
        }
    }

    void TryPickup()
    {
        Collider[] hits = Physics.OverlapSphere(
            palletHoldPoint.position,
            0.35f
        );

        foreach (Collider hit in hits)
        {
            if (!hit.CompareTag("Pallet")) continue;

            Rigidbody rb = hit.attachedRigidbody;
            if (!rb) continue;

            carriedPallet = rb;
            rb.isKinematic = true;
            rb.transform.SetParent(palletHoldPoint);
            rb.transform.localPosition = Vector3.zero;
            rb.transform.localRotation = Quaternion.identity;
            break;
        }
    }

    void Release()
    {
        carriedPallet.transform.SetParent(null);
        carriedPallet.isKinematic = false;
        carriedPallet = null;
    }
}
