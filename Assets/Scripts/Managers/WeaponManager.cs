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
    }
    
    public void GrantWeapon(Weapon weapon)
    {
        Weapon addedWeapon = Instantiate(weapon, weaponParent);
        activeWeapons.Add(addedWeapon);
    }



}
