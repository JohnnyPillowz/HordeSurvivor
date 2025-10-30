using UnityEngine;

[CreateAssetMenu(fileName = "PlayerUpgradeStatsSO", menuName = "Scriptable Objects/PlayerUpgradeStatsSO")]

public class PlayerUpgradeStatsSO : UpgradeStatsSO
{
    [SerializeField] public PlayerUpgradeTypes upgradeType;

    public void ApplyUpgrade(float amount)
    {
        Player.Instance.stats.Upgrade(upgradeType, amount);
    }
}
