using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    [SerializeField] private GameObject upgradesScreen;
    [SerializeField] private List<GameObject> upgradeCardSlots = new List<GameObject>();
    
    [Header("Upgrade Settings")]
    [SerializeField] public int maxNumOfUpgrades = 3;
    [SerializeField] private List<UpgradeCard> upgradeCards = new List<UpgradeCard>();
    
    private List<UpgradeCard> activeUpgradeCards = new List<UpgradeCard>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
            Debug.Log("UpgradesManager destroyed");
        }
    }
    
    public void TurnOnUpgradesScreen()
    {
        upgradesScreen.SetActive(true);

        for (int i = 0; i < upgradeCardSlots.Count; i++)
        {
            UpgradeCard upgradeCard = RollUpgradeCard();
            UpgradeCard instantiatedCard = Instantiate(upgradeCard, upgradeCardSlots[i].transform);
            activeUpgradeCards.Add(instantiatedCard);
            PauseManager.Instance.Pause();
        }
    }

    private UpgradeCard RollUpgradeCard()
    {
        UpgradeCard upgradeCard = upgradeCards[Random.Range(0, upgradeCards.Count)];
        return upgradeCard;
    }
    
    public void TurnOffUpgradesScreen()
    {
        upgradesScreen.SetActive(false);

        foreach (UpgradeCard instantiatedCard in activeUpgradeCards)
        {
            Destroy(instantiatedCard);
        }
        
        PauseManager.Instance.Resume();
    }
}
