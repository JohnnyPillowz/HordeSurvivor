using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        //WeaponParameters
        [SerializeField] private float damage = 2f;
        [SerializeField] private float fireRate = 1f;
        [SerializeField] private float range = 15f;
        [SerializeField] private float knockback = 10f;
        //Accessors
        protected float Damage { get => damage; set => damage = value; }
        protected float FireRate {  get => fireRate; set => fireRate = value; }
        protected float Range { get => range; set => range = value; }
        protected float Knockback { get => knockback; set => knockback = value; }
    
        [SerializeField] public List<UpgradeStatsSO> upgradeStatsSOs = new List<UpgradeStatsSO>();
    
        private Coroutine firingCoroutine;
        
        public virtual void StartFiring()
        {
            if (firingCoroutine == null)
            {
                firingCoroutine = StartCoroutine(FiringRoutine());
            }
        }

        public virtual void StopFiring()
        {
            if (firingCoroutine != null)
            {
                StopCoroutine(firingCoroutine);
                firingCoroutine = null;
            }
        }

        protected virtual IEnumerator FiringRoutine()
        {
            while (true)
            {
                Fire();
                yield return new WaitForSeconds(1/FireRate);
            }
        }

        protected virtual void Fire()
        {
        
        }

        public virtual void Upgrade(WeaponUpgradeTypes upgradeType, float amount)
        {
            switch (upgradeType)
            {
                case WeaponUpgradeTypes.damage: damage *= amount; break;
                case WeaponUpgradeTypes.fireRate: fireRate *= amount; break;
                case WeaponUpgradeTypes.range: range *= amount; break;
                case WeaponUpgradeTypes.knockback: knockback *= amount; break;
            }
        }
    }
}
