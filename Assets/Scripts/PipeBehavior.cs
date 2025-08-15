using System.Collections;
using UnityEngine;
using static UnityEngine.KeyCode;

public class PipeBehavior : MonoBehaviour
{
    public GameObject myExit;
    [SerializeField] private float exitVelocity = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("lalalala");
        StartCoroutine(EnterPipe(other.gameObject));
    }

    IEnumerator EnterPipe(GameObject player)
    {
        var _playerRigidbody = player.GetComponent<Rigidbody2D>();
        // Offset the exit position in the direction the exit is facing
        Vector2 exitPosition = (Vector2)myExit.transform.position + (Vector2)myExit.transform.up * 1.5f;
        player.transform.position = exitPosition;
        _playerRigidbody.linearVelocity = Vector2.zero;
        _playerRigidbody.AddForce(myExit.transform.up * exitVelocity, ForceMode2D.Impulse);
        yield break;
    }
}