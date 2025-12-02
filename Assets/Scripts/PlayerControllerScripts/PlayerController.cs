using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float runSpeed = 1.35f;
    public float startRunDelay = 2.45f;
    public Rigidbody2D rb;
    public Animator animator;
    public float moveTimer;
    public bool isPlayerMoving;
    public bool isPlayerRunning;
    private Vector2 movement;
    private Vector2 playerLastDirection;
    private Vector2 previousDirection;

    void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();

        if (movement.magnitude > 0)
        {
            if (isPlayerMoving == false)
            {
                isPlayerMoving = true;
                moveTimer = 0f;
                isPlayerRunning = false;
            }

            if (movement != previousDirection)
            {
                playerLastDirection = movement.normalized;
                previousDirection = movement;
            }
        }
        else
        {
            isPlayerMoving = false;
            moveTimer = 0f;
            isPlayerRunning = false;
        }
    }
    

    void Update()
    {
        animator.SetFloat("Horizontal", playerLastDirection.x);
        animator.SetFloat("Vertical", playerLastDirection.y);
        animator.SetBool("isPlayerMoving", isPlayerMoving);

        if (isPlayerMoving == true)
        {
            moveTimer += Time.deltaTime;

            if (moveTimer >= startRunDelay)
            {
                isPlayerRunning = true;
            }
        }
        else
        {
            isPlayerRunning = false;
        }
    }

    void FixedUpdate()
    {
        float playerCurrentSpeed;
        if (isPlayerRunning)
        {
            playerCurrentSpeed = runSpeed;
        }
        else
        {
            playerCurrentSpeed = moveSpeed;
        }

        rb.MovePosition(rb.position + movement * playerCurrentSpeed * Time.fixedDeltaTime);
    }
}
