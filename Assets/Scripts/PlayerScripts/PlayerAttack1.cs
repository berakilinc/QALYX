using UnityEngine;

public class PlayerAttack1 : MonoBehaviour
{
    public GameObject skill1Prefab;
    public Transform skillOutPoint;
    private float lastLookX = 1;
    private float lastLookY = 0;
    
    public float playerSkill1Timer = 0.45f;
    public float currentTimer = 0;

    void Update()
    {
        currentTimer -= Time.deltaTime;
        float moveInput = Input.GetAxisRaw("Horizontal");
        float moveInput2 = Input.GetAxisRaw("Vertical");

        if (moveInput != 0 || moveInput2 != 0)
        {
            lastLookX = moveInput;
            lastLookY = moveInput2;
        }

        if (Input.GetMouseButtonDown(0) && currentTimer <= 0)
        {
            SkillOut();
            currentTimer = playerSkill1Timer;
        }
    }

    void SkillOut()
    {
        float skillAng = 0;

        if (lastLookX > 0 && lastLookY > 0)
        {
            skillAng = 45;
        }
        else if (lastLookX < 0 && lastLookY > 0)
        {
            skillAng = 135;
        }
        else if (lastLookX < 0 && lastLookY < 0)
        {
            skillAng = 225;
        }
        else if (lastLookX > 0 && lastLookY < 0)
        {
            skillAng = 315;
        }
        else if (lastLookX > 0)
        {
            skillAng = 0;
        }
        else if (lastLookX < 0)
        {
            skillAng = 180;
        }
        else if (lastLookY > 0)
        {
            skillAng = 90;
        }
        else if (lastLookY < 0)
        {
            skillAng = 270;
        }

        Instantiate(skill1Prefab, skillOutPoint.position, Quaternion.Euler(0, 0, skillAng));
    }
}