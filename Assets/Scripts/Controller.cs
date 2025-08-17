using Unity.Mathematics;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float torque;
    [SerializeField] private float airForce;
    //[SerializeField] private float jumpHeight;
    [SerializeField] private float maxControlAngularSpeed;
    [SerializeField] private float minBouncySpeed;
    [SerializeField] private PhysicsMaterial2D bouncyMaterial;
    [SerializeField] private PhysicsMaterial2D normalMaterial;
    //private float jumpVelocity;
    private bool isGrounded;
    private float oneOverRadius;
    
    public bool isTeleporting = false;

    private void Awake()
    {
        oneOverRadius = 1/GetComponent<CircleCollider2D>().radius;
        //jumpVelocity = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
    }

    private const float oneOverPi = 1 / math.PI;

    private void FixedUpdate()
    {
        if (rb.bodyType != RigidbodyType2D.Dynamic)
        {
            return;
        }
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float angSpeed = rb.angularVelocity;
        if (angSpeed > maxControlAngularSpeed)
            horizontalInput = math.clamp(horizontalInput, 0, 1);
        else if (angSpeed < -maxControlAngularSpeed)
            horizontalInput = math.clamp(horizontalInput, -1, 0);

        if (!isGrounded)
        {
            rb.AddForce(airForce * horizontalInput * Vector2.right);
            rb.angularVelocity = -360 * rb.linearVelocityX * oneOverRadius * oneOverPi * 0.5f;
            return;
        }

        float totalTorque = -horizontalInput * torque;
        rb.AddTorque(totalTorque);

        LimitBounceOnLowSpeed();
    }

    private void LimitBounceOnLowSpeed()
    {
        rb.sharedMaterial = rb.linearVelocity.magnitude < minBouncySpeed ? normalMaterial : bouncyMaterial;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
