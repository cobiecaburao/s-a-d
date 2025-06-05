using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public float moveSpeed;

    public Rigidbody2D rb;

    private Vector2 moveDirection;

    private float xPosLastFrame;
    // Update is called once per frame
    void Update()
    {
        // Processing inputs
        ProcessInputs();
        flipCharacterX();
    }

    void FixedUpdate()
    {
        // Physics calculations
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY);

        if (moveX != 0 || moveY !=0)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        //bool flipped = moveDirection.x < 0;
        //this.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));

    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        
    }

    void flipCharacterX()
    {
        if (transform.position.x > xPosLastFrame) {
            // we are moving right
            spriteRenderer.flipX = false;
        }
        else if(transform.position.x < xPosLastFrame) {
            // we are moving left
            spriteRenderer.flipX = true;
        }

        xPosLastFrame = transform.position.x;
    }
}
