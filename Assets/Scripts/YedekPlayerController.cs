using UnityEngine;

public class YedekPlayerConroller : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;

    public float jumpingPower;
    private bool isJumpPressed, isJumping, isDoubleJumpAvailable;
    private float airTime;

    public Transform groundCheck;
    public LayerMask groundLayer;

    public float gravityScale, fallingGravityScale, jumpingGravityScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpingPower = 15;
        TriggerRunAnimation();

        gravityScale = 4.3f;
        fallingGravityScale = 5;
        jumpingGravityScale = 2.7f;
        rb.gravityScale = gravityScale;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Spike"))
        {
            Destroy(gameObject);
            Debug.Log("Game Over");
        }
    }

    void Update()
    {
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
        isJumpPressed = Input.GetButtonDown("Jump");
        isJumping = Input.GetButton("Jump");
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);


        if (isJumpPressed && isGrounded())
        {
            Jump();
        }

        if (!isGrounded())
        {
            airTime += Time.fixedDeltaTime;
            Debug.Log(airTime);

            if (isJumpPressed && airTime > 0.05f && isDoubleJumpAvailable)
            {
                Debug.Log(airTime);
                isDoubleJumpAvailable = false;
                DoubleJump();
            }
        }

        if (isGrounded() && rb.linearVelocityY < 0.1)
        {
            isDoubleJumpAvailable = true;
            airTime = 0;
            TriggerRunAnimation();
        }

        if (isJumping && rb.linearVelocity.y >= 0 && isDoubleJumpAvailable)
            rb.gravityScale = jumpingGravityScale;

        else if (rb.linearVelocity.y >= 0)
        {
            //rb.linearVelocity -= new Vector2(0, 0.7f);
            rb.gravityScale = gravityScale;
        }

        else if (rb.linearVelocity.y < 0)
        {
            //rb.linearVelocity -= new Vector2(0, 0.9f);
            rb.gravityScale = fallingGravityScale;
        }

        if (transform.position.y < -25)
        {
            Destroy(gameObject);
        }
    }

    bool isGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.55f, 0.15f)
            , CapsuleDirection2D.Horizontal, 0, groundLayer);
    }
    void Jump()
    {
        //rb.linearVelocity = new(rb.linearVelocity.x, jumpPower);
        rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);
        TriggerJumpAnimation();
    }
    void DoubleJump()
    {
        rb.gravityScale = gravityScale;
        rb.linearVelocity = new Vector2(0, jumpingPower);
        TriggerJumpAnimation();
    }

    private void TriggerRunAnimation()
    {
        animator.SetBool("isRunning", true);
        animator.SetBool("isJumping", false);
    }
    private void TriggerJumpAnimation()
    {
        animator.SetBool("isJumping", true);
        animator.SetBool("isRunning", false);
    }

}
