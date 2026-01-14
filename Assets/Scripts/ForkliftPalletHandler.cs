using UnityEngine;

public class ForkliftPalletHandler : MonoBehaviour
{
    public Transform palletHoldPoint;
    public KeyCode pickupKey = KeyCode.E;
    public KeyCode releaseKey = KeyCode.Q;

    GameObject currentPallet;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q pressed");
        }

        if (Input.GetKeyDown(pickupKey) && currentPallet)
        {
            PickupPallet();
        }

        if (Input.GetKeyDown(releaseKey) && currentPallet)
        {
            ReleasePallet();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pallet"))
        {
            currentPallet = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pallet"))
        {
            if (currentPallet == other.gameObject)
                currentPallet = null;
        }
    }

    void PickupPallet()
    {
        currentPallet.transform.SetParent(palletHoldPoint);
        currentPallet.transform.localPosition = Vector3.zero;
    }

    void ReleasePallet()
    {
        currentPallet.transform.SetParent(null);
        currentPallet = null;
    }
}
