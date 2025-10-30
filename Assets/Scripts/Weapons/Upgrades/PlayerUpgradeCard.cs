using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerUpgradeCard : UpgradeCard
{ 
    private List<RolledPlayerUpgradeData> rolledUpgradesData = new List<RolledPlayerUpgradeData>();

    private Player player;
    private UpgradeManager upgradeManager;

    protected override void Start()
    {
        player = Player.Instance;
        upgradeManager = UpgradeManager.Instance;
        RollUpgrade();
    }
    protected override void RollUpgrade()
    {
        int numberOfUpgrades = Random.Range(0, upgradeManager.maxNumOfUpgrades);
        List<PlayerUpgradeStatsSO> possibleUpgrades = new List<PlayerUpgradeStatsSO>(player.stats.upgrades);

        for (int i = 0; i <= numberOfUpgrades; i++)
        {
            //manage upgrade option
            PlayerUpgradeStatsSO upgradeSO = possibleUpgrades[Random.Range(0, possibleUpgrades.Count)];
            possibleUpgrades.Remove(upgradeSO);
            
            //roll amount
            float amount = (Mathf.Round(Random.Range(upgradeSO.minUpgrade, upgradeSO.maxUpgrade) * 100)) / 100;
            float amountForUpgrade = 1 + amount;

            //instantiate and update text 
            TMP_Text upgradeText = Instantiate(UpgradeTextPrefab, UpgradeTextTransform).GetComponent<TMP_Text>();
            int amountForText = Mathf.RoundToInt((amount) * 100);
            string generatedUpgradeText = upgradeSO.upgradeName + ": " + amountForText + "%";
            upgradeText.text = generatedUpgradeText;

            //store data
            RolledPlayerUpgradeData rolledUpgradeData = new RolledPlayerUpgradeData
            {
                PlayerUpgradeSo = upgradeSO,
                amount = amountForUpgrade
            };
            rolledUpgradesData.Add(rolledUpgradeData);
        }
    }

    public override void ApplyUpgrade()
    {
        foreach (RolledPlayerUpgradeData rolledUpgradeData in rolledUpgradesData)
        {
            rolledUpgradeData.PlayerUpgradeSo.ApplyUpgrade(rolledUpgradeData.amount);
        }
        rolledUpgradesData.Clear();
        upgradeManager.TurnOffUpgradesScreen();
    }
}
