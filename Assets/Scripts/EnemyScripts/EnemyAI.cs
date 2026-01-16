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
    
    public GameObject deathEffect;
    public AudioClip deathSound;
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.2f;
    public Color damageColor = Color.red;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isKnockback = false;


    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
        }

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }

        spawner = FindAnyObjectByType<EnemySpawner>();
        
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (player == null || isKnockback) 
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

    public void TakeDamage(int amount, Vector2 damageSourcePos)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(ApplyKnockback(damageSourcePos));
        }
    }

    IEnumerator ApplyKnockback(Vector2 sourcePos)
    {
        isKnockback = true;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = damageColor;

            Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
            Vector2 randomDir = directions[Random.Range(0, directions.Length)];
            rb.linearVelocity = randomDir * knockbackForce;

            yield return new WaitForSeconds(knockbackDuration);

            if (spriteRenderer != null) spriteRenderer.color = originalColor;
            rb.linearVelocity = Vector2.zero;
            isKnockback = false;
        }
    }

    void Die()
    {
        if (spawner != null)
        {
            spawner.OnEnemyKilled();
        }

        if (deathEffect != null)
        {
            Vector3 effectPosition = new Vector3(transform.position.x, transform.position.y + 0.44f, transform.position.z);
            Instantiate(deathEffect, effectPosition, Quaternion.identity);
        }

        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
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