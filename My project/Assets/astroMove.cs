using UnityEngine;

public class AstroMove : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private int jumpCount = 0; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space)) Jump(); }



    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal"); 
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }

    void Jump()
    {
        if (jumpCount < 2)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0; 
        }
    }
}
