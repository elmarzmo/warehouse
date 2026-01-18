using UnityEngine;

public class ForkliftLift : MonoBehaviour
{
    public float liftSpeed = 1.5f;
    public float maxLiftOffset = 1.8f; // how high forks can go from start

    float startY;
    float minHeight;
    float maxHeight;

    void Start()
    {
        // Record the correct resting position
        startY = transform.localPosition.y;
        minHeight = startY;
        maxHeight = startY + maxLiftOffset;
    }

    void Update()
    {
        float input = 0f;

        if (Input.GetKey(KeyCode.R)) input = 1f;
        if (Input.GetKey(KeyCode.F)) input = -1f;

        Vector3 pos = transform.localPosition;
        pos.y += input * liftSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minHeight, maxHeight);

        transform.localPosition = pos;
    }
}