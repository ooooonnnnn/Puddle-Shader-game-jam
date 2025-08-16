using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Rigidbody2D targetRb;
    [SerializeField] private float multiplier = 1f;
    [SerializeField] private float minFov = 60f;
    [SerializeField] private float maxFov = 64f;
    [SerializeField] private float forceFovDuration = 1f;
    [SerializeField] private CinemachineFollow cinemachineFollow;
    private float fovVelocity = 0f;
    private bool fovBySpeed = true;
    private float targetFov;
    private float forceFovPerc = 0;

    private void OnValidate()
    {
        cinemachineFollow = GetComponent<CinemachineFollow>();
    }

    void FixedUpdate()
    {
        targetFov = 60f + targetRb.linearVelocity.magnitude * multiplier;
        if (!fovBySpeed)
        {
            targetFov = Mathf.Lerp(targetFov, minFov, forceFovPerc);
            print($"Target: {targetFov}");
        }

        camera.fieldOfView = Mathf.Clamp(
            Mathf.SmoothDamp(camera.fieldOfView, targetFov, ref fovVelocity, 1f),
            minFov, maxFov
        );

        if (!fovBySpeed)
            print(camera.fieldOfView);
    }

    public void ForceMinFov()
    {
        //cinemachineFollow.ForceCameraPosition(cinemachineFollow.FollowOffset, Quaternion.identity);
        Vector3 cameraPos = transform.position;
        cameraPos.z = -10f; 
        transform.position = cameraPos;
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
