using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    public bool IsDashing {get; private set; }

    private bool canDash = true;
    private Rigidbody2D rb;
    private PlayerController playerController;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }

    void OnDash(InputValue value)
    {
        if (value.isPressed && canDash && !IsDashing)
        {
            StartCoroutine(DashRoutine());
        }
    }

    private IEnumerator DashRoutine()
    {
        IsDashing = true;
        canDash = false;

        Vector2 dashDirection = playerController.playerLastDirection;
        if (dashDirection == Vector2.zero)
        {
            dashDirection = Vector2.right;
        }

        rb.linearVelocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);
        rb.linearVelocity = Vector2.zero;
        IsDashing = false;
        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }


}
