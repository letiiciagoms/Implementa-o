using UnityEngine;

public class CameraController2D : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void LateUpdate()
    {
        if (target == null)
        {
            // Tenta achar o player pela tag
            GameObject player = GameObject.FindWithTag("Player");

            if (player != null)
                target = player.transform;
            else
                return;
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}

