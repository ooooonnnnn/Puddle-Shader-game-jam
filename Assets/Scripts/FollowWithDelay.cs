using UnityEngine;

public class FollowWithDelay : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Range(0f, 1f)] private float followOffset = 0.01f;
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, target.position, followOffset * Time.deltaTime);
    }
}
