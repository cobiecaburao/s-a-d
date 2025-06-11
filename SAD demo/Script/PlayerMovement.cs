using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TrailRenderer _trailRenderer;

    [SerializeField] private float _dashingVelocity = 14f;
    [SerializeField] private float _dashingTime = 0.2f;
    [SerializeField] private float _dashCooldown = 1f;
    private bool _isDashing;
    private bool _canDash = true;



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
        if (_isDashing)
        {
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        var dashInput = Input.GetButtonDown("Dash");

        moveDirection = new Vector2(moveX, moveY);

        if (moveX != 0 || moveY !=0)
        {
            _animator.SetBool("IsMoving", true);
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }
        if(dashInput && _canDash)
        {
            StartCoroutine(Dash());
        }

    }

    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;
        rb.linearVelocity = new Vector2(moveDirection.x * _dashingVelocity, moveDirection.y * _dashingVelocity);
        _trailRenderer.emitting = true;
        yield return new WaitForSeconds(_dashingTime);
        _trailRenderer.emitting = false;
        _isDashing = false;
        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }

    void Move()
    {
        if (_isDashing)
        {
            return;
        }

        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        
    }

    void flipCharacterX()
    {
        if (transform.position.x > xPosLastFrame) {
            // we are moving right
            _spriteRenderer.flipX = false;
        }
        else if(transform.position.x < xPosLastFrame) {
            // we are moving left
            _spriteRenderer.flipX = true;
        }

        xPosLastFrame = transform.position.x;
    }
}
