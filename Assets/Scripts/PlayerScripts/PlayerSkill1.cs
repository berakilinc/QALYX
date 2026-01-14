using UnityEngine;

public class PlayerSkill1 : MonoBehaviour
{
    public float speed = 15f;
    public int damage = 10;
    
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyAI enemy = hitInfo.GetComponent<EnemyAI>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage, transform.position);
            Destroy(gameObject);
        }
    }

}
