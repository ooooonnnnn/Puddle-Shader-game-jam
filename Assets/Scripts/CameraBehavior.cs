using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Rigidbody2D targetRb;
    [SerializeField] private float multiplier = 1f;
    private float fovVelocity = 0f;

    void FixedUpdate()
    {
        float targetFov = 60f + targetRb.linearVelocity.magnitude * multiplier;
        camera.fieldOfView = Mathf.Clamp(
            Mathf.SmoothDamp(camera.fieldOfView, targetFov, ref fovVelocity, 0.3f),
            60f, 90f
        );
    }
}
