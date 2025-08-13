using UnityEngine;

public class Tile : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;

    public float destroyPosition;

    void Start()
    {
        speed = 13;
        destroyPosition = -50;

        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = speed * Vector2.left;
        
    }

    void Update()
    {
        if (transform.position.x < destroyPosition)
        {
            Destroy(gameObject);
            //Debug.Log(transform.position.x);
        }
    }
}
