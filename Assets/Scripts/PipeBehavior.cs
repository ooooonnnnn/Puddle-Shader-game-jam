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
            StartCoroutine(MyEnterPipe(other.gameObject.GetComponent<PathFollower>())); 
            TriggerInteractionDaddy.NotifyInteraction(this);
        }
    }
    
    //private void OnTriggerStay2D(Collider2D other)
    //{
    //        StartCoroutine(myEnterPipe(other.GetComponent<PathFollower>()));
    //}

    private IEnumerator MyEnterPipe(PathFollower pathFollower)
    {
        if (!isTeleporting)
        {
            isTeleporting = true;
            pathFollower.StartFollow(thisEndPoint);
            // Wait a short time to prevent retriggering
            yield return new WaitForSeconds(0.2f);
            isTeleporting = false;
        }
    }

    //IEnumerator EnterPipe(GameObject player)
    //{
    //    if (myExit == null)
    //        yield break;

    //    isTeleporting = true;

    //    PathFollower pathFollower = player.GetComponent<PathFollower>();

    //    var playerRigidbody = player.GetComponent<Rigidbody2D>();

    //    // Get original speed before teleport
    //    float originalSpeed = playerRigidbody.linearVelocity.magnitude;

    //    // Move player to exit position
    //    Vector2 exitPosition = (Vector2)myExit.transform.position + (Vector2)myExit.transform.up * 1.5f;
    //    player.transform.position = exitPosition;

    //    // Set velocity in the exit's direction, with the new speed
    //    float newSpeed = originalSpeed + exitVelocity;
    //    playerRigidbody.linearVelocity = (Vector2)myExit.transform.up * newSpeed;

    //    // Wait a short time to prevent retriggering
    //    yield return new WaitForSeconds(0.2f);
    //    isTeleporting = false;
    //}
}