using UnityEngine;

public class ForkliftLift : MonoBehaviour
{
    [Header("Lift Settings")]
    public float liftSpeed = 1.2f;
    public float minHeight = 0.1f;
    public float maxHeight = 2.5f;

    Vector3 startLocalPos;

    void Start()
    {
        startLocalPos = transform.localPosition;
    }

    void Update()
    {
        float input = 0f;

        if (Input.GetKey(KeyCode.R)) input = 1f;
        if (Input.GetKey(KeyCode.F)) input = -1f;

        if (Mathf.Abs(input) < 0.01f) return;

        Vector3 pos = transform.localPosition;
        pos.y += input * liftSpeed * Time.deltaTime;

        float minY = startLocalPos.y + minHeight;
        float maxY = startLocalPos.y + maxHeight;

        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.localPosition = pos;
    }
}

