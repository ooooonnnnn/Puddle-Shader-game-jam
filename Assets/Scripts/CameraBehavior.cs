using System.Collections;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Rigidbody2D targetRb;
    [SerializeField] private float multiplier = 1f;
    [SerializeField] private float minFov = 60f;
    [SerializeField] private float maxFov = 64f;
    [SerializeField] private float forceFovDuration = 1f;
    private float fovVelocity = 0f;
    private bool fovBySpeed = true;
    private float targetFov;
    private float forceFovPerc = 0;

    void FixedUpdate()
    {
        targetFov = 60f + targetRb.linearVelocity.magnitude * multiplier;
        if (!fovBySpeed)
        {
            targetFov = Mathf.Lerp(targetFov, minFov, forceFovPerc);
        }

        camera.fieldOfView = Mathf.Clamp(
            Mathf.SmoothDamp(camera.fieldOfView, targetFov, ref fovVelocity, 0.3f),
            minFov, maxFov
        );
    }

    public void ForceMinFov()
    {
        StartCoroutine(ForceMinFovDuration());
    }

    private IEnumerator ForceMinFovDuration()
    {
        float startTime = Time.time;
        float endTime = startTime + forceFovDuration;
        fovBySpeed = false;
        while (Time.time < endTime)
        {
            forceFovPerc = Mathf.InverseLerp(endTime, startTime, Time.time);
            yield return null;
        }
        fovBySpeed = true;
    }
}
