using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponAttack : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private BoxCollider2D _hitbox;

    [SerializeField] private float _attackTime = 0.2f;
    //[SerializeField] private float _comboTime = 1f;
    private bool _canAttack = true;
    public float damage = 1;

    // Update is called once per frame
    void Update()
    {
        var attackInput = Input.GetButtonDown("Attack");

        if(attackInput && _canAttack)
        {
            StartCoroutine(Attack());
            StartCoroutine(AttackBox());
        }

    }

    private IEnumerator Attack()
    {
        _canAttack = false;
        _spriteRenderer.enabled = true;
        yield return new WaitForSeconds(_attackTime);
        _spriteRenderer.enabled = false;
        yield return new WaitForSeconds(_attackTime);
        _canAttack = true;
    }

    private IEnumerator AttackBox()
    {
        _hitbox.enabled = true;
        yield return new WaitForSeconds(_attackTime);
        _hitbox.enabled = false;
    }

    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            // deal damage to the enemy
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Health -= damage;
                enemy._spriteRenderer.color = Color.red;
                yield return new WaitForSeconds(_attackTime);
                enemy._spriteRenderer.color = Color.white;
            }

        }
    }
}
