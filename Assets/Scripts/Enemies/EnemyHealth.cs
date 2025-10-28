using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO enemyStats;

    private EnemyManager enemyManager;
    private float currentHealth;

    public event Action<EnemyHealth> OnDeath; 
    private void Start()
    {
        currentHealth = enemyStats.health;
        enemyManager = EnemyManager.Instance;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke(this);
        Destroy(gameObject);
        enemyManager.enemies.Remove(this.gameObject);
    }
}
