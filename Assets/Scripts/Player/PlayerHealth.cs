using System;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int playerStartHealth = 15;
    [SerializeField] private TMP_Text playerHealthText;
    
    private int playerMaxHealth;
    private int currentHealth;

    private void Start()
    {
        currentHealth = playerStartHealth;
        playerMaxHealth = playerStartHealth;
        playerHealthText.text = playerMaxHealth.ToString();
    }

    public void TakeDamage(float damage)
    {
        int damageTaken = -Mathf.RoundToInt(damage);
        AdjustHealth(damageTaken);
    }

    public void Heal(int heal)
    {
        AdjustHealth(heal);
    }

    public void IncreaseMaxHealth(int increase)
    {
        playerMaxHealth += increase;
        playerHealthText.text = playerMaxHealth.ToString();
    }

    private void AdjustHealth(int amount)
    {
        if (currentHealth + amount < playerMaxHealth && currentHealth + amount > 0)
        {
            currentHealth += amount;
        }
        else if (currentHealth + amount >= playerMaxHealth)
        {
            currentHealth = playerMaxHealth;
        }
        else if (currentHealth + amount <= 0)
        {
            currentHealth = 0;
            Die();
        }
        playerHealthText.text = currentHealth.ToString();
    }

    private void Die()
    {
        Destroy(this.gameObject);
        PauseManager.Instance.Pause();
    }
}
