using UnityEngine;
using Weapons;

[CreateAssetMenu(fileName = "UpgradeStatsSO", menuName = "Scriptable Objects/UpgradeStatsSO")]
public class UpgradeStatsSO : ScriptableObject
{
    [SerializeField] public string upgradeName;
    [SerializeField] public float minUpgrade = 0.05f;
    [SerializeField] public float maxUpgrade = 0.2f;

    [SerializeField] public WeaponUpgradeTypes upgradeType;

    public void ApplyUpgrade(Weapon weapon, float amount)
    {
        weapon.Upgrade(upgradeType, amount);
    }
}
