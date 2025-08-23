using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;

    public float jumpingPower;
    private bool isJumpPressed, isJumping, isDoubleJumpAvailable;

    public Transform groundCheck;
    public LayerMask groundLayer;

    public float defaultGravityScale, fallingGravityScale, jumpingGravityScale;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    public GameDirector gameDirector;

    private float defaultPositionX;
    private float positionTime = 4f;
    private float positionTimeCounter;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpingPower = 15;

        defaultPositionX = transform.position.x;
        positionTimeCounter = positionTime;
        //TriggerRunAnimation();

        defaultGravityScale = 4.3f;
        fallingGravityScale = 5;
        jumpingGravityScale = 2.7f;
        rb.gravityScale = defaultGravityScale;

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Spike"))
        {
            audioManager.PlaySFX(audioManager.takeHit);
            DestroyPlayer();
        }

    }

    void Update()
    {
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
        isJumpPressed = Input.GetButtonDown("Jump");
        isJumping = Input.GetButton("Jump");

        //jumpbuffer space tuþu kontrolü yerine geçiyor, karakterin yere deðmeden zýplamasýný saðlýyor
        if (isJumpPressed)
            jumpBufferCounter = jumpBufferTime;  //eðer havadayken space tuþuna basýlý tutursa deðeri baþlatýr, karakter yere iner inmez zýplar

    }

    void FixedUpdate()
    {
        if (!gameDirector.levelManager.isPlayerMoving)
        {
            return;
        }

        //if (rb.linearVelocity.x > 0.2f || rb.linearVelocity.x < -0.2f)
        //{
        //    positionTimeCounter = positionTime;  sürekli ileri atýlýnca köstek oluyor, hep yapmasa daha ii
        //    Debug.Log(rb.linearVelocity.x);
        //}

        if (transform.position.x <= defaultPositionX - 5f)
        {                                                                       
            positionTimeCounter -= Time.fixedDeltaTime;  //start'da tanýmladýk sýkýntý yok
        }

        if (positionTimeCounter <= 0f)
        {
            DisableColliders();
            rb.MovePosition(new Vector2(transform.position.x + 0.2f, transform.position.y + 0.1f));
            TriggerJumpAnimation();

            //Debug.Log("Repositioning");

            if (transform.position.x >= defaultPositionX)
            {
                positionTimeCounter = positionTime;
                rb.linearVelocity = new Vector2(0, 0);
                EnableColliders();
                //Debug.Log("Start point X");
            }
        }


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


        if (coyoteTimeCounter > 0f && !isJumping) //yere deðdiðinde double jump sýfýrlansýn
        {
            isDoubleJumpAvailable = false; //eðer zýplamada çift zýplamayý kullanmazsak bu true kalacak, sonraki zýplamalarda çakýþabilir
        }

        if (jumpBufferCounter > 0f) //isground, getbuttondown
        {
            if (coyoteTimeCounter > 0f || isDoubleJumpAvailable)
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

        if (transform.position.x < -25)
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

        if (rb.linearVelocity.y <= 0)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            
        else if (rb.linearVelocity.y > 0)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower + rb.linearVelocity.y / 3f);

        if (isDoubleJumpAvailable)
            audioManager.PlaySFX(audioManager.doubleJump);
        else
            audioManager.PlaySFX(audioManager.jump);

        TriggerJumpAnimation();
    }

    private void DestroyPlayer()
    {
        //Destroy(gameObject);
        //gameDirector.levelManager.StopPlayer();
        audioManager.PlaySFX(audioManager.die);
        gameDirector.levelManager.StopGame(); //artýk playerin update'i çalýþmayacak
        TriggerDeathAnimation();
        rb.bodyType = RigidbodyType2D.Static; //öldükten sonra hareket olmasýn
        Debug.Log("Game Over");
    }

    private void EnableColliders()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
    }

    private void DisableColliders()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
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

    private void TriggerDeathAnimation()
    {
        animator.SetBool("isDied", true);
        animator.SetBool("isJumping", false);
        animator.SetBool("isRunning", false);
    }

}
