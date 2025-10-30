using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Weapons;
using Random = UnityEngine.Random;

public class WeaponUpgradeCard : UpgradeCard
{
    private List<RolledWeaponUpgradeData> rolledUpgradesData = new List<RolledWeaponUpgradeData>();
    
    private WeaponManager weaponManager;
    private UpgradeManager upgradeManager;

    private Weapon rolledWeapon;
    
    protected override void Start()
    {
        weaponManager = WeaponManager.Instance;
        upgradeManager = UpgradeManager.Instance;
        RollUpgrade();
    }

    protected override void RollUpgrade()
    {
        //Roll weapon
        rolledWeapon = weaponManager.ActiveWeapons[Random.Range(0, weaponManager.ActiveWeapons.Count)];
        
        int numberOfUpgrades = Random.Range(0, upgradeManager.maxNumOfUpgrades);
        List<WeaponUpgradeStatsSO> possibleUpgrades = new List<WeaponUpgradeStatsSO>(rolledWeapon.upgradeStatsSOs);

        for (int i = 0; i <= numberOfUpgrades; i++)
        {
            //manage upgrade option
            WeaponUpgradeStatsSO upgradeSO = possibleUpgrades[Random.Range(0, possibleUpgrades.Count)];
            possibleUpgrades.Remove(upgradeSO);
            
            //roll amount
            float amount = (Mathf.Round(Random.Range(upgradeSO.minUpgrade, upgradeSO.maxUpgrade)*100))/100f;
            float amountForUpgrade = 1 + amount;
            
            //instantiate and update text
            TMP_Text upgradeText = Instantiate(UpgradeTextPrefab, UpgradeTextTransform).GetComponent<TMP_Text>();
            int amountForText = Mathf.RoundToInt((amount) * 100);
            string generatedUpgradeText = upgradeSO.upgradeName + ": " + amountForText + "%";
            upgradeText.text = generatedUpgradeText;
            
            //store data
            RolledWeaponUpgradeData rolledUpgradeData = new RolledWeaponUpgradeData
            {
                WeaponUpgradeSo = upgradeSO,
                amount = amountForUpgrade
            };
            rolledUpgradesData.Add(rolledUpgradeData);
        }
    }

    public override void ApplyUpgrade()
    {
        foreach (RolledWeaponUpgradeData rolledUpgradeData in rolledUpgradesData)
        {
            rolledUpgradeData.WeaponUpgradeSo.ApplyUpgrade(rolledWeapon, rolledUpgradeData.amount);
        }
        rolledUpgradesData.Clear();
        upgradeManager.TurnOffUpgradesScreen();
    }
}
