using UnityEngine.XR;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public Transform pointA;
    public Transform pointB;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    private bool facingRight = true;

    public Transform player;  // Reference to the player
    public float detectionRange = 5f;  // Detection range for attack
    public float attackCooldown = 1f;  // Time between attacks
    private bool canAttack = true;  // To handle cooldown

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();  // Get Animator component
        currentPoint = pointB;  // Start moving towards pointB
    }

    void Update()
    {
        Move();
        UpdateAnimation();  // Update animations

        // Check if player is in range and attack
        if (IsPlayerInRange() && canAttack)
        {
            Attack();
        }
    }

    private void Move()
    {
        float direction = Mathf.Sign(currentPoint.position.x - transform.position.x);
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        if (Mathf.Abs(transform.position.x - currentPoint.position.x) < 0.5f)
        {
            SwitchDirection();
        }
    }

    private void UpdateAnimation()
    {
        float moveSpeed = Mathf.Abs(rb.linearVelocity.x);
        anim.SetFloat("Speed", moveSpeed);
    }

    private void SwitchDirection()
    {
        currentPoint = (currentPoint == pointA) ? pointB : pointA;
        Flip();
        rb.linearVelocity = new Vector2((facingRight ? 1 : -1) * speed, rb.linearVelocity.y);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private bool IsPlayerInRange()
    {
        // Check if player is within detection range
        float distance = Vector2.Distance(transform.position, player.position);
        return distance <= detectionRange;
    }

    private void Attack()
    {
        // Trigger attack animation
        anim.SetTrigger("Attack");

        // Handle attack cooldown to prevent spam attacks
        canAttack = false;
        Invoke("ResetAttackCooldown", attackCooldown);
    }

    private void ResetAttackCooldown()
    {
        canAttack = true;
    }
}
