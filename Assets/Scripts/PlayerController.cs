using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;

    public float jumpingPower;
    private bool isJumpPressed, isJumpReleased, isJumping, isDoubleJumpAvailable;
    private float airTime;

    public Transform groundCheck;
    public LayerMask groundLayer;

    public float defaultGravityScale, fallingGravityScale, jumpingGravityScale;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpingPower = 15;
        TriggerRunAnimation();

        defaultGravityScale = 4.3f;
        fallingGravityScale = 5;
        jumpingGravityScale = 2.7f;
        rb.gravityScale = defaultGravityScale;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Spike"))
        {
            DestroyPlayer();
        }
    }

    void Update()
    {
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
        isJumpPressed = Input.GetButtonDown("Jump");
        isJumping = Input.GetButton("Jump");
        isJumpReleased = Input.GetButtonUp("Jump");

        //jumpbuffer space tu�u kontrol� yerine ge�iyor, karakterin yere de�meden z�plamas�n� sa�l�yor
        if (isJumpPressed)
            jumpBufferCounter = jumpBufferTime;  //e�er havadayken space tu�una bas�l� tutursa de�eri ba�lat�r, karakter yere iner inmez z�plar
        

    }

    void FixedUpdate()
    { 
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

        jumpBufferCounter -= Time.fixedDeltaTime;


        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime;

            if (rb.linearVelocity.y < 0.1f) TriggerRunAnimation();
        }
        else
        {
            coyoteTimeCounter -= Time.fixedDeltaTime;
        }


        if(coyoteTimeCounter > 0f && !isJumping) //yere de�di�inde double jump s�f�rlans�n
        {
            isDoubleJumpAvailable = false;
        }

        if (jumpBufferCounter > 0f) //isground, getbuttondown
        {
            if(coyoteTimeCounter > 0f || isDoubleJumpAvailable)
            {
                Jump();
                isDoubleJumpAvailable = !isDoubleJumpAvailable;
            }

            jumpBufferCounter = 0f;
            coyoteTimeCounter = 0f;
        }


        if (isJumping && rb.linearVelocity.y >= 0 && isDoubleJumpAvailable)
            rb.gravityScale = jumpingGravityScale;

        else if (rb.linearVelocity.y >= 0)
        {
            //rb.linearVelocity -= new Vector2(0, 0.7f);
            rb.gravityScale = defaultGravityScale;
        }

        else if (rb.linearVelocity.y < 0)
        {
            //rb.linearVelocity -= new Vector2(0, 0.9f);
            rb.gravityScale = fallingGravityScale;
        }


        if (transform.position.y < -25)
        {
            DestroyPlayer();
        }

    }


    bool isGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.55f, 0.15f)
            ,CapsuleDirection2D.Horizontal, 0, groundLayer);
    }

    void Jump()
    {
        rb.gravityScale = defaultGravityScale;

        if(rb.linearVelocity.y <= 0)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        else if(rb.linearVelocity.y > 0)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower + rb.linearVelocity.y/3f);
            //Burayla oyna    

        TriggerJumpAnimation();
    }

    private void DestroyPlayer()
    {
        Destroy(gameObject);
        Debug.Log("Game Over");
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


    //if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f) //isground, getbuttondown
    //{
    //    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);

    //    jumpBufferCounter = 0f;
    //}
    //if(isJumpReleased && rb.linearVelocity.y > 0f)
    //{
    //    rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);

    //    coyoteTimeCounter = 0f; //uzun z�plama yapmas�n� istiyorsak coyotetime'� burada s�f�rlamal�y�z, yukar�da
    //                            //s�f�rlan�rsa uzun z�plama olmaz hemen a�a�� �ak�l�r(maz, space tu�undan �ekildi�i
    //                            //zaman bu if'e giriyor
    //}


    //if (isJumpReleased && rb.linearVelocity.y > 0f)
    //{
    //    rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
    //}
}
