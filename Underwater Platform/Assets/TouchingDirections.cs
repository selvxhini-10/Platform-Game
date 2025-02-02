using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    
    public ContactFilter2D castFilter;
    Animator animator;
    public float groundDistance = 0.05f;

    CapsuleCollider2D touchingCol;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];

    [SerializeField]
    private bool _isGrounded = true;

    public bool IsGrounded {  get
        {
            return _isGrounded;
        } private set
        {
            _isGrounded = value;
            animator.SetBool("isGrounded", value);
        }
    }
    public bool IsGround { get; private set; }
    public bool IsOnWall { get; internal set; }

    private void Awake()
    {

            touchingCol = GetComponent<CapsuleCollider2D>();

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
   void Update()
    {
        //touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance);  
        IsGround = touchingCol.Cast(Vector2.down,castFilter, groundHits, groundDistance) > 0;
    }
}
