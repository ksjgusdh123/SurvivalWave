using Unity.VisualScripting;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;         
    public float smoothSpeed = 5f;

    float originPitch;
    float yaw;

    void Awake()
    {
        originPitch = transform.eulerAngles.x;
        yaw = transform.eulerAngles.y;
    }

    void LateUpdate()
    {
        if (!target) return;

        Vector3 targetPos = target.position;

        Quaternion rot = Quaternion.Euler(originPitch, yaw, 0f);
        Vector3 rotatedOffset = rot * offset;

        Vector3 desiredPosition = targetPos + rotatedOffset;
        transform.position = desiredPosition; 
        transform.rotation = rot;
    }

    public void UpdateCamera(Vector2 look)
    {
        yaw += look.x;
    }
}
