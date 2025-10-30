using UnityEngine;
using Weapons;

[CreateAssetMenu(fileName = "WeaponUpgradeStatsSO", menuName = "Scriptable Objects/WeaponUpgradeStatsSO")]
public class WeaponUpgradeStatsSO : UpgradeStatsSO
{
    [SerializeField] public WeaponUpgradeTypes upgradeType;

    public void ApplyUpgrade(Weapon weapon, float amount)
    {
        weapon.Upgrade(upgradeType, amount);
    }
}
