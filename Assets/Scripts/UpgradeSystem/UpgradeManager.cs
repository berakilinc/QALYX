using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public PlayerAttack1 playerAttack1;
    public PlayerController playerController;
    public float playerBaseMoveSpeed = 1f;
    public int playerRegenBooster = 1;

    public GameObject upgradePanel;
    public EnemySpawner spawner;

    public TMP_Text firstButtonText;
    public TMP_Text secondButtonText;

    int firstRandomID;
    int secondRandomID;


    void Start()
    {
        upgradePanel.SetActive(false);
    }

    public void OpenUpgradePanel()
    {
        upgradePanel.SetActive(true);
        Time.timeScale = 0f;
        GenerateRandomOptions();
    }

    void GenerateRandomOptions()
    {
        firstRandomID = Random.Range(0, 5);
        secondRandomID = Random.Range(0, 5);

        while (secondRandomID == firstRandomID)
        {
            secondRandomID = Random.Range(0, 5);
        }

        firstButtonText.text = GetTextByID(firstRandomID);
        secondButtonText.text = GetTextByID(secondRandomID);
    }

    string GetTextByID(int id)
    {
        if (id == 0) return "Max Health +1";
        if (id == 1) return "Current Speed +5%";
        if (id == 2) return "Start Speed +10%";
        if (id == 3) return "Instant Health Regen";
        if (id == 4) return "Regen Booster +1";
        return "";
    }

    public void ClickFirstButton()
    {
        ExecuteUpgrade(firstRandomID);
    }

    public void ClickSecondButton()
    {
        ExecuteUpgrade(secondRandomID);
    }

    void ExecuteUpgrade(int id)
    {
        if (id == 0) PlayerMaxHealthIncrease();
        else if (id == 1) PlayerCurrentSpeedIncrease();
        else if (id == 2) PlayerStartSpeedIncrease();
        else if (id == 3) PlayerHealthRegen();
        else if (id == 4) PlayerHealthRegenBooster();
    }

    private void FinalizeUpgrade()
    {
        upgradePanel.SetActive(false);
        Time.timeScale = 1f;
        spawner.ResumeSpawner();
    }

    public void PlayerMaxHealthIncrease()
    {
        playerHealth.playerMaxHealth += 1;
        playerHealth.playerCurrentHealth += 1;
        playerHealth.UpdateUI();
        Debug.Log("PlayerMaxHealthIncrease");
        FinalizeUpgrade();
    }

    public void PlayerCurrentSpeedIncrease()
    {
        playerController.moveSpeed = (playerController.moveSpeed * 5f / 100f) + playerController.moveSpeed;
        Debug.Log("PlayerCurrentSpeedIncrease");
        FinalizeUpgrade();
    }

    public void PlayerStartSpeedIncrease()
    {
        playerController.moveSpeed += playerBaseMoveSpeed * 0.1f;
        Debug.Log("PlayerStartSpeedIncrease");
        FinalizeUpgrade();
    }

    public void PlayerHealthRegen()
    {
        playerHealth.playerCurrentHealth += playerRegenBooster;
        if (playerHealth.playerCurrentHealth > playerHealth.playerMaxHealth) playerHealth.playerCurrentHealth = playerHealth.playerMaxHealth;
        playerHealth.UpdateUI();
        Debug.Log("PlayerHealthRegen");
        FinalizeUpgrade();
    }

    public void PlayerHealthRegenBooster()
    {
        playerRegenBooster ++;
        Debug.Log("PlayerHealthRegenBooster");
        FinalizeUpgrade();
    }




}
