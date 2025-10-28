using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Weapons;
using Random = UnityEngine.Random;

public class UpgradeCard : MonoBehaviour
{
    [Header("Upgrade Card Text")]
    [SerializeField] private GameObject upgradeTextPrefab;
    [SerializeField] private Transform upgradeTextTranform;
    
    private List<RolledUpgradeData> rolledUpgradesData = new List<RolledUpgradeData>();
    
    private WeaponManager weaponManager;

    private Weapon rolledWeapon;
    
    void Start()
    {
        weaponManager = WeaponManager.Instance;
        RollUpgrade();
    }

    public void RollUpgrade()
    {
        //Roll weapon
        rolledWeapon = weaponManager.ActiveWeapons[Random.Range(0, weaponManager.ActiveWeapons.Count)];
        
        int numberOfUpgrades = Random.Range(0, weaponManager.maxNumOfUpgrades);
        List<UpgradeStatsSO> possibleUpgrades = new List<UpgradeStatsSO>(rolledWeapon.upgradeStatsSOs);

        for (int i = 0; i <= numberOfUpgrades; i++)
        {
            //manage upgrade option
            UpgradeStatsSO upgradeStatsSO = possibleUpgrades[Random.Range(0, possibleUpgrades.Count)];
            possibleUpgrades.Remove(upgradeStatsSO);
            
            //roll amount
            float amount = (Mathf.Round(Random.Range(upgradeStatsSO.minUpgrade, upgradeStatsSO.maxUpgrade)*100))/100f;
            float amountForUpgrade = 1 + amount;
            
            //instantiate and update text
            TMP_Text upgradeText = Instantiate(upgradeTextPrefab, upgradeTextTranform).GetComponent<TMP_Text>();
            int amountForText = Mathf.RoundToInt((amount) * 100);
            string generatedUpgradeText = upgradeStatsSO.upgradeName + ": " + amountForText + "%";
            upgradeText.text = generatedUpgradeText;
            
            //store data
            RolledUpgradeData rolledUpgradeData = new RolledUpgradeData
            {
                upgradeSO = upgradeStatsSO,
                amount = amountForUpgrade
            };
            rolledUpgradesData.Add(rolledUpgradeData);
        }
    }

    public void ApplyUpgrade()
    {
        foreach (RolledUpgradeData rolledUpgradeData in rolledUpgradesData)
        {
            rolledUpgradeData.upgradeSO.ApplyUpgrade(rolledWeapon, rolledUpgradeData.amount);
        }
        rolledUpgradesData.Clear();
        weaponManager.TurnOffUpgradesScreen();
    }
}
