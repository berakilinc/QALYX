using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int playerMaxHealth = 3;
    public int playerCurrentHealth;
    public TMP_Text healthDisplay;

    public AudioClip damageSound;
    private AudioSource audioSource;
    public GameObject gameOverPanel;

    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        UpdateUI();
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void RestartGame() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
            GameOver();
        }
    }

    void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
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
