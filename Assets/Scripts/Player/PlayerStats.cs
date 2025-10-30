using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public float minePower;
    [SerializeField] public float moveSpeed;
    [SerializeField] public int maxHealth;

    private float maxHealthFloat;
    
    [Header("Upgrades")]
    [Range(1, 5)]
    [SerializeField] public List<PlayerUpgradeStatsSO> upgrades = new List<PlayerUpgradeStatsSO>();
    
    private PlayerHealth playerHealth;
    private void Start()
    {
        maxHealthFloat = (float)maxHealth;
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void Upgrade(PlayerUpgradeTypes upgradeType, float amount)
    {
        switch (upgradeType)
        {
            case PlayerUpgradeTypes.minePower: minePower *= amount; break; 
            case PlayerUpgradeTypes.movementSpeed: moveSpeed *= amount; break;
            case PlayerUpgradeTypes.maxHealth: AdjustHealth(amount); break;
            //add for every new stat
        }
    }

    public void AdjustHealth(float amount)
    {
        maxHealthFloat *= amount;
        
        int newMaxHealth = Mathf.RoundToInt(maxHealthFloat);
        int healAmount = newMaxHealth - maxHealth;
        maxHealth = newMaxHealth;
        playerHealth.Heal(healAmount);
    }
}
