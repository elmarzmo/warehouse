using UnityEngine;

public class ForkliftCameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the forklift's transform

    [Header("Offset")]
    public Vector3 offset = new Vector3(0f, 2.2f, -4.5f); // Offset between the camera and the forklift

    [Header("Smoothness")]
    public float positionSmoothnessTime = 0.15f;
    public float rotationSmoothnessTime = 0.1f;

    [Header("Clipping")]
    public float collisionRadius = 0.3f;
    public LayerMask collisionLayers;

    Vector3 currentVelocity;

    void LateUpdate()
    {
        if (!target) return;

        Vector3 desiredPosition = target.TransformPoint(offset);

        float minHeight = target.position.y + 0.6f;

        if (desiredPosition.y < minHeight)
        {
            desiredPosition.y = minHeight;
        }

        // camera collision check
        if (Physics.SphereCast(
            target.position + Vector3.up*1.2f,
            collisionRadius,
            (desiredPosition - target.position).normalized,
            out RaycastHit hit,
            offset.magnitude,
            collisionLayers))
        {
            desiredPosition = hit.point + hit.normal * collisionRadius;
        }

        // Smoothly move the camera to the desired position
            Vector3 finalPosition = Vector3.SmoothDamp(
            transform.position,
            desiredPosition, 
            ref currentVelocity,
            positionSmoothnessTime);

      
        transform.position = finalPosition;

        // smooth look at
        Quaternion targetRotation = Quaternion.LookRotation(target.position + Vector3.up - transform.position);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSmoothnessTime);
    }
}
