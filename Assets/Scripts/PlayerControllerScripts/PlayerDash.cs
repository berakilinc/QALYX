using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    public CameraShakeMechanic cameraShakeMec;

    public float cameraShakeRateA;
    public float cameraShakeRateB;

    public bool IsDashing {get; private set; }

    private bool canDash = true;
    private Rigidbody2D rb;
    private PlayerController playerController;

    public TextMeshProUGUI dashCountDownTxt;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }

    void Start()
    {
        if (dashCountDownTxt != null) 
            dashCountDownTxt.text = "Ready";
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

        if (cameraShakeMec != null)
        {
            StartCoroutine(cameraShakeMec.CameraShakeMec(cameraShakeRateA,cameraShakeRateB));
        }

        Vector2 dashDirection = playerController.playerLastDirection;
        if (dashDirection == Vector2.zero)
        {
            dashDirection = Vector2.right;
        }

        rb.linearVelocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);
        rb.linearVelocity = Vector2.zero;
        IsDashing = false;

        float timer = dashCooldown;

        while (timer > 0)
        {
            timer -= Time.deltaTime;

            if (dashCountDownTxt != null)
            {
                dashCountDownTxt.text = timer.ToString("F2");
            }
            yield return null;
            dashCountDownTxt.text ="Ready";
        }

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }


}
