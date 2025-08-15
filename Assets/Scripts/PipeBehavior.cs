using System.Collections;
using UnityEngine;
using static UnityEngine.KeyCode;

public class PipeBehavior : MonoBehaviour
{
    [SerializeField] private Color connectedColor;
    [SerializeField] private Color disconnectedColor;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public PipeBehavior myExit
    {
        get => _myExit;
        set
        {
            _myExit = value;
            UpdateColor();
        }
    }

    [SerializeField] private PipeBehavior _myExit;
    [SerializeField] private float exitVelocity = 5f;

    private void UpdateColor()
    {
        spriteRenderer.color = _myExit != null ? connectedColor : disconnectedColor;
    }

    private void Awake()
    {
        UpdateColor();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<Controller>().isTeleporting)
            StartCoroutine(EnterPipe(other.gameObject));
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.GetComponent<Controller>().isTeleporting)
            StartCoroutine(EnterPipe(other.gameObject));
    }

    IEnumerator EnterPipe(GameObject player)
    {
        if (myExit == null)
            yield break;

        player.GetComponent<Controller>().isTeleporting = true;

        var playerRigidbody = player.GetComponent<Rigidbody2D>();

        // Get original speed before teleport
        float originalSpeed = Mathf.Abs(playerRigidbody.linearVelocity.magnitude);
        Debug.Log($"{gameObject.name}: {originalSpeed}");

        // Move player to exit position
        Vector2 exitPosition = (Vector2)myExit.transform.position + (Vector2)myExit.transform.up * 1.5f;
        player.transform.position = exitPosition;

        // Set velocity in the exit's direction, with the new speed
        float newSpeed = originalSpeed + exitVelocity;
        playerRigidbody.linearVelocity = (Vector2)myExit.transform.up * newSpeed;

        yield return new WaitForSeconds(0.2f);
        player.GetComponent<Controller>().isTeleporting = false;
    }
}