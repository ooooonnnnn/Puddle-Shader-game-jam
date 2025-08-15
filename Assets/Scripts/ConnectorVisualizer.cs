using System.Collections;
using UnityEngine;

public class ConnectorVisualizer : MonoBehaviour
{
    [SerializeField] private float turningTime;

    public void ChangeAngle(float angle)
    {
        StopAllCoroutines();
        StartCoroutine(Rotate(angle));
    }

    private IEnumerator Rotate(float targetAngle)
    {
        float currentAngle = transform.eulerAngles.z;
        float startTime = Time.time;

        while (Time.time - startTime < turningTime)
        {
            float t = (Time.time - startTime) / turningTime;
            float angle = Mathf.LerpAngle(currentAngle, targetAngle, t);
            transform.rotation = Quaternion.Euler(0, 0, angle);
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0, 0, targetAngle);
    }
}
