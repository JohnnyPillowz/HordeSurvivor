using System;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private TMP_Text playerHealthText;
    [SerializeField] private Player player;

    private int currentHealth;

    private void Start()
    {
        currentHealth = player.stats.maxHealth;
        playerHealthText.text = player.stats.maxHealth.ToString();
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

    private void AdjustHealth(int amount)
    {
        if (currentHealth + amount < player.stats.maxHealth && currentHealth + amount > 0)
        {
            currentHealth += amount;
        }
        else if (currentHealth + amount >= player.stats.maxHealth)
        {
            currentHealth = player.stats.maxHealth;
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
