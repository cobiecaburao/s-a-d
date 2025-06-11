using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public SpriteRenderer _spriteRenderer;

    public float _damageIFrame = 1.5f;
    [SerializeField] private float _attackTime = 0.2f;
    public int damage;
    public PlayerHealth playerHealth;
    private bool _hasAttacked = false;

    public Transform playerTransform;
    public Rigidbody2D rb;
    public float moveSpeed = 2f;
    Vector2 moveDir;


    private void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }
    private void Update()
    {
        if (playerTransform)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            moveDir = direction;
            rb.linearVelocity = new Vector2(moveDir.x, moveDir.y) * moveSpeed;
        }
    }

    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                Defeated();
            }
        }
        get
        {
            return health;
        }
    }
    public float health;

    public void TakeDamage(float damage)
    {
        _spriteRenderer.color = Color.red;
    }

    public void Defeated()
    {
        _spriteRenderer.color = Color.black;
        Destroy(gameObject);
    }
    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (_hasAttacked)
        {
            yield return new WaitForSeconds(_damageIFrame);
            _hasAttacked = false;
        }
        if (other.tag == "Player")
        {
            // deal damage to the enemy
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                _hasAttacked = true;
                player.TakeDamage(damage);
                player._spriteRenderer.color = Color.red;
                yield return new WaitForSeconds(_attackTime);
                player._spriteRenderer.color = Color.white;
            }

        }
    }




}
