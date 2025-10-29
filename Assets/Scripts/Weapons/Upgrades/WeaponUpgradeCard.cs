using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Weapons;
using Random = UnityEngine.Random;

public class WeaponUpgradeCard : MonoBehaviour
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
        List<WeaponUpgradeStatsSO> possibleUpgrades = new List<WeaponUpgradeStatsSO>(rolledWeapon.upgradeStatsSOs);

        for (int i = 0; i <= numberOfUpgrades; i++)
        {
            //manage upgrade option
            WeaponUpgradeStatsSO weaponUpgradeStatsSo = possibleUpgrades[Random.Range(0, possibleUpgrades.Count)];
            possibleUpgrades.Remove(weaponUpgradeStatsSo);
            
            //roll amount
            float amount = (Mathf.Round(Random.Range(weaponUpgradeStatsSo.minUpgrade, weaponUpgradeStatsSo.maxUpgrade)*100))/100f;
            float amountForUpgrade = 1 + amount;
            
            //instantiate and update text
            TMP_Text upgradeText = Instantiate(upgradeTextPrefab, upgradeTextTranform).GetComponent<TMP_Text>();
            int amountForText = Mathf.RoundToInt((amount) * 100);
            string generatedUpgradeText = weaponUpgradeStatsSo.upgradeName + ": " + amountForText + "%";
            upgradeText.text = generatedUpgradeText;
            
            //store data
            RolledUpgradeData rolledUpgradeData = new RolledUpgradeData
            {
                WeaponUpgradeSo = weaponUpgradeStatsSo,
                amount = amountForUpgrade
            };
            rolledUpgradesData.Add(rolledUpgradeData);
        }
    }

    public void ApplyUpgrade()
    {
        foreach (RolledUpgradeData rolledUpgradeData in rolledUpgradesData)
        {
            rolledUpgradeData.WeaponUpgradeSo.ApplyUpgrade(rolledWeapon, rolledUpgradeData.amount);
        }
        rolledUpgradesData.Clear();
        weaponManager.TurnOffUpgradesScreen();
    }
}
