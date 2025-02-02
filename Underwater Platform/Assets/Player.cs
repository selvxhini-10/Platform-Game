using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody2D),typeof(TouchingDirections))]
public class Player : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 9f;
    Vector2 moveInput;
    TouchingDirections touchingDirections;

    private Animator playerAnimation;

    private Vector3 respawnPoint;
    public GameObject fallDetector;

    public float jumpSpeed = 18f;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
        private bool isTouchingGround;
    public Text scoreText;

    void Start()
    {
        playerAnimation = GetComponent<Animator>();
        respawnPoint = transform.position;
        scoreText.text = "Score: " + Scoring.totalScore;
    }
    public float CurrentMoveSpeed {  get
        {
            if (IsMoving)
            {
                if (IsRunning)
                {
                    return runSpeed;

                } else
                {
                    return walkSpeed;
                }
            } else
            {
                return 0;
            }
        }
    }

    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }
    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        } set
        {
            _isRunning = value;
            animator.SetBool("isRunning", value);

        }
    }

    public bool _isFacingRight = true;
    
    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if (_isFacingRight != value)
            {
                //flip the local scale
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    Rigidbody2D rb;
    Animator animator;

    private void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
        }

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
        } else if (collision.tag == "Checkpoint")
        {
            respawnPoint = transform.position;
        } else if (collision.tag == "Item")
        {
            Scoring.totalScore += 1;
            scoreText.text = "Score: " + Scoring.totalScore;
            collision.gameObject.SetActive(false);
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.linearVelocity.y);
        rb.angularVelocity = 0f; // Prevents rotation

        animator.SetFloat("yVelocity", rb.linearVelocity.y);
       // animator.SetBool("isGrounded", touchingDirections.IsGround);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }


    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            //face right 
            IsFacingRight = true;
        } else if (moveInput.x <0 && IsFacingRight)
        {
            //face left 
            IsFacingRight = false;
        }
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        } else if (context.canceled)
        {
            IsRunning = false;
        }
    }
  
}
