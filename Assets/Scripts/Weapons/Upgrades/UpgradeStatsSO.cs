using UnityEngine;

public abstract class UpgradeStatsSO : ScriptableObject
{
    [SerializeField] public string upgradeName;
    [SerializeField] public float minUpgrade = 0.05f;
    [SerializeField] public float maxUpgrade = 0.2f;
}
