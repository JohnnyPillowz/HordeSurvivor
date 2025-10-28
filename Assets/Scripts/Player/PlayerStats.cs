using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public float minePower;
    [SerializeField] public float moveSpeed;


    public void Upgrade(PlayerUpgradeTypes upgradeType, float amount)
    {
        switch (upgradeType)
        {
            case PlayerUpgradeTypes.MinePower: minePower *= amount; break; 
            case PlayerUpgradeTypes.MoveSpeed: moveSpeed *= amount; break;
            //add for every new stat
        }
    }
}
