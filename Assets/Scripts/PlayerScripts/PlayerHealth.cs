using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int playerMaxHealth = 3;
    public int playerCurrentHealth;
    public TMP_Text healthDisplay;

    public AudioClip damageSound;
    private AudioSource audioSource;

    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        audioSource = GetComponent<AudioSource>();
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {        
        Debug.Log("Old :" + playerCurrentHealth);
        playerCurrentHealth -= damage;
        Debug.Log("New :" + playerCurrentHealth);
        UpdateUI();
        
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        if (playerCurrentHealth <= 0)
        {
            Debug.Log("Test x<=0");
        }
    }
    public void UpdateUI()
    {
        if (healthDisplay != null)
        {
            healthDisplay.text = "HP: " + playerCurrentHealth + " / " + playerMaxHealth;
        }
    }


}
