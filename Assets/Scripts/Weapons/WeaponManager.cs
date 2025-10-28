using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Weapons;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    
    [Header("Weapons")]
    [SerializeField] private List<Weapon> allWeapons = new List<Weapon>();
    public List<Weapon> AllWeapons { get => allWeapons; private set => allWeapons = value; }
    private List<Weapon> activeWeapons = new List<Weapon>();
    [HideInInspector] public List<Weapon> ActiveWeapons { get => activeWeapons; private set => activeWeapons = value; }
    [SerializeField] private Transform weaponParent;
    
    [Header("Upgrades")]
    [SerializeField] private GameObject upgradesScreen;
    [SerializeField] private List<GameObject> upgradeCardSlots = new List<GameObject>();
    [SerializeField] private GameObject upgradeCardPrefab;
    [SerializeField] public int maxNumOfUpgrades = 3;
    
    private List<GameObject> activeUpgradeCards = new List<GameObject>();

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

    private void Start()
    {
        GrantWeapon(allWeapons[0]);
        GrantWeapon(allWeapons[1]);
        //StartCoroutine(UpgradesScreenRoutine());
        TurnOffUpgradesScreen();
    }

    private IEnumerator UpgradesScreenRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            TurnOnUpgradesScreen();
        }
    }
    
    public void GrantWeapon(Weapon weapon)
    {
        Weapon addedWeapon = Instantiate(weapon, weaponParent);
        activeWeapons.Add(addedWeapon);
    }
    public void TurnOnUpgradesScreen()
    {
        upgradesScreen.SetActive(true);

        for (int i = 0; i < upgradeCardSlots.Count; i++)
        {
            GameObject instantiatedCard = Instantiate(upgradeCardPrefab, upgradeCardSlots[i].transform);
            activeUpgradeCards.Add(instantiatedCard);
            PauseManager.Instance.Pause();
        }
    }

    public void TurnOffUpgradesScreen()
    {
        upgradesScreen.SetActive(false);

        foreach (GameObject instantiadedCard in activeUpgradeCards)
        {
            Destroy(instantiadedCard);
        }
        
        PauseManager.Instance.Resume();
    }
}
