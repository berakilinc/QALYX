using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public int damageAmount = 10;
    public int maxHealth = 20;
    public float yPosDecreaser =0.44f;
    private int currentHealth;
    private Transform player;
    private Animator animator;
    private Rigidbody2D rb;
    private EnemySpawner spawner;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
        }

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        spawner = FindObjectOfType<EnemySpawner>();
        
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (player == null) 
        {
            return;
        }

        Vector2 playerTargetedPos = player.position;
        playerTargetedPos.y -= yPosDecreaser;

        Vector2 direction = (playerTargetedPos - (Vector2)transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;

        if (animator != null)
        {
            animator.SetFloat("InputX", direction.x);
            animator.SetFloat("InputY", direction.y);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (spawner != null)
        {
            spawner.OnEnemyKilled();
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth healthScript = collision.gameObject.GetComponent<PlayerHealth>();
            if (healthScript != null)
            {
                healthScript.TakeDamage(damageAmount);
            }
        }
    }
}