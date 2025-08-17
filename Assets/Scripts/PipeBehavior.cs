using System.Collections;
using UnityEngine;

public class PipeBehavior : MonoBehaviour
{
    [SerializeField] private Color connectedColor;
    [SerializeField] private Color disconnectedColor;
    [SerializeField] private SpriteRenderer spriteRenderer;
    //[SerializeField] private float maxExitSpeed = 25f;
    [SerializeField] private PathEndPoint thisEndPoint;
    private bool isTeleporting = false;

    //[SerializeField] private float exitVelocity = 5f;

    private void OnValidate()
    {
        thisEndPoint = GetComponent<PathEndPoint>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTeleporting)
        {
            MyDebugLogManager.LogMessage($"{gameObject.name} triggered at time {Time.time}");
            StartCoroutine(EnterPipe(other.GetComponent<PathFollower>()));
        }
    }
    
    //private void OnTriggerStay2D(Collider2D other)
    //{
    //        StartCoroutine(myEnterPipe(other.GetComponent<PathFollower>()));
    //}

    private IEnumerator EnterPipe(PathFollower pathFollower)
    {
        MyDebugLogManager.LogMessage($"EnterPipe called");
        if (!pathFollower)
            MyDebugLogManager.LogMessage("PathFollower is null");

        if (!isTeleporting)
        {
            isTeleporting = true;
            pathFollower.StartFollow(thisEndPoint);
            // Wait a short time to prevent retriggering
            yield return new WaitForSeconds(0.2f);
            isTeleporting = false;
        }
    }
}