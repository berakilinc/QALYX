using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerMaxHealth = 100;
    private int playerCurrentHealth;

    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    public void TakeDamage(int damage)
    {        
        Debug.Log("Old :" + playerCurrentHealth);
        playerCurrentHealth -= damage;
        Debug.Log("New :" + playerCurrentHealth);

        if (playerCurrentHealth <= 0)
        {
            Debug.Log("Test x<=0");
        }
    }


}
