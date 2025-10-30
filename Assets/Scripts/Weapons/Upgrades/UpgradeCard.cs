using UnityEngine;

public abstract class UpgradeCard : MonoBehaviour
{
    [Header("Upgrade Card Text")] 
    [SerializeField] private GameObject upgradeTextPrefab;
    [SerializeField] private Transform upgradeTextTransform;
    
    //Accessors
    protected GameObject UpgradeTextPrefab { get => upgradeTextPrefab; set => upgradeTextPrefab = value; }
    protected Transform UpgradeTextTransform { get => upgradeTextTransform; set => upgradeTextTransform = value; }
    protected virtual void Start()
    {
        RollUpgrade();
    }

    protected virtual void RollUpgrade()
    {
        
    }

    public virtual void ApplyUpgrade()
    {
        
    }
}
