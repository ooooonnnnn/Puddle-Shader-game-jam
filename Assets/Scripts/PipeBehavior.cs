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

    private void OnTriggerStay2D(Collider2D other)
    {
        StartCoroutine(EnterPipe(other.gameObject));
    }

    IEnumerator EnterPipe(GameObject player)
    {
        if (myExit == null)
            yield break; // Exit if there is no exit defined

        var _playerRigidbody = player.GetComponent<Rigidbody2D>();
        // Offset the exit position in the direction the exit is facing
        Vector2 exitPosition = (Vector2)myExit.transform.position + (Vector2)myExit.transform.up * 1.5f;
        player.transform.position = exitPosition;
        _playerRigidbody.linearVelocity = Vector2.zero;
        _playerRigidbody.AddForce(myExit.transform.up * exitVelocity, ForceMode2D.Impulse);
        yield break;
    }
}