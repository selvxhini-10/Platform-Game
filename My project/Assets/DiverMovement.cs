using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private int jumpCount = 0; 
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2) {
            Jump();
        }
    }

    void FixedUpdate() {
        Move();
    }

    void Move(){
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }

    void Jump() {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); 
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpCount++; 
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")){
            
            jumpCount = 0; 
        }
    }

    void OnTriggerEnter2D(Collider2D other){
    if (other.gameObject.CompareTag("PlasticCan")){
        FindFirstObjectByType<PlasticCanCounter>().AddCan();
        Destroy(other.gameObject);
    }
}

}
