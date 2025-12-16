using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerObjectTarget;
    
    public float smoothSpeed = 0.25f;
    public Vector3 offset;

    void LateUpdate()
    {
        if (playerObjectTarget == null) return;
        {
            Vector3 desiredPosition = playerObjectTarget.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
