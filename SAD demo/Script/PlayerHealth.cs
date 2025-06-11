using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] public SpriteRenderer _spriteRenderer;
    public int _maxHealth = 5;
    public int _health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _health = _maxHealth;
    }
     
    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            _animator.SetBool("IsDead", true);
        }
    }
}
