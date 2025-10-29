using UnityEngine;
using Weapons;

[CreateAssetMenu(fileName = "WeaponUpgradeStatsSO", menuName = "Scriptable Objects/WeaponUpgradeStatsSO")]
public class WeaponUpgradeStatsSO : ScriptableObject
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
