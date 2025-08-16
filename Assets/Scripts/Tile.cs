using UnityEngine;
using UnityEngine.InputSystem;

public class Tile : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed;
    private float destroyPosition;
    private Vector2 defaultSpeed;
    private static bool IsGameStarted;

    void Start()
    {
        speed = 13;
        destroyPosition = -50;

        rb = GetComponent<Rigidbody2D>();
        defaultSpeed = speed * Vector2.left;

        if (IsGameStarted)
            rb.linearVelocity = defaultSpeed;
    }

    void Update()
    {
        if (transform.position.x < destroyPosition)
        {
            Destroy(gameObject);
            //Debug.Log(transform.position.x);
        }

        //if (Keyboard.current.anyKey.wasPressedThisFrame)
        //{
        //    StartGame(); ///
        //}

    }

    public void StartPlatform()
    {
        rb.linearVelocity = defaultSpeed;
        IsGameStarted = true;
    }

    public void StopPlatform()
    {
        rb.linearVelocity = Vector2.zero;
        IsGameStarted = false;
    }

    public static void StopPlatforms()
    {
        IsGameStarted = false;
    }
}
