using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float enemySpeed = 3f;
    public int enemyDamage = 10;

    private Transform playerTransform;
    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (playerTransform == null)
        {
            return;
        }
        Vector2 playerfixYposition = playerTransform.position;
        playerfixYposition.y -= 0.44f;
        Vector2 direction = (playerfixYposition - (Vector2)transform.position).normalized;
        rb.linearVelocity = direction * enemySpeed;

        UpdateAnimation(direction);
    }

    void UpdateAnimation(Vector2 dir)
    {
        if (animator != null)
        {
            animator.SetFloat("InputX", dir.x);
            animator.SetFloat("InputY", dir.y);
        }
    }




}
