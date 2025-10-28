using System;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class WeaponGarlic : Weapon
    {
        [SerializeField] private GameObject visual;
        [SerializeField] private SphereCollider coll;
        
        private List<EnemyHealth> enemiesInRange = new List<EnemyHealth>();
        
        private void Start()
        {
            StartFiring();
        }
        
        protected override void Fire()
        {
            foreach (EnemyHealth enemyHealth in enemiesInRange.ToArray()) //.ToArray might need removal if performance is a problem
            {
                enemyHealth.TakeDamage(Damage);
                
                Rigidbody otherRB = enemyHealth.GetComponent<Rigidbody>();
                if (otherRB)
                {
                    Vector3 knockbackDir = (enemyHealth.transform.position - transform.position).normalized;
                    otherRB.linearVelocity = knockbackDir * Knockback;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (!enemyHealth) { return; }

            enemyHealth.OnDeath += HandleEnemyDeath;
            enemiesInRange.Add(enemyHealth);
        }

        private void OnTriggerExit(Collider other)
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (!enemyHealth) { return; }
            
            enemiesInRange.Remove(enemyHealth);
            enemyHealth.OnDeath -= HandleEnemyDeath;
        }
        
        private void HandleEnemyDeath(EnemyHealth enemyHealth)
        {
            enemiesInRange.Remove(enemyHealth);
        }

        public override void Upgrade(WeaponUpgradeTypes upgradeType, float amount)
        {
            switch (upgradeType)
            {
                case WeaponUpgradeTypes.damage: Damage *= amount; break;
                case WeaponUpgradeTypes.fireRate: FireRate *= amount; AdjustRotationSpeed();  break;
                case WeaponUpgradeTypes.range: Range *= amount; AdjustSize(); break;
                case WeaponUpgradeTypes.knockback: Knockback *= amount; break;
            }
        }

        [ContextMenu("Adjust Size")]
        private void AdjustSize()
        {
            coll.radius = Range;
            visual.transform.localScale = new Vector3(2 * Range, 2 * Range, 1);
        }

        private void AdjustRotationSpeed()
        {
            visual.GetComponent<Spin>().spinSpeed = FireRate * 10;
        }

        private void OnValidate()
        {
            AdjustSize();
            AdjustRotationSpeed();
        }
    }
}
