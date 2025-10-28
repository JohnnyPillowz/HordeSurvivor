using UnityEngine;

namespace Weapons
{
    public class WeaponBow : Weapon
    {
        [SerializeField] private GameObject projectile;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private bool isPiercing = false;
  
        private EnemyManager enemyManager;

        private void Start()
        {
            enemyManager = EnemyManager.Instance;
            StartFiring();
        }

        protected override void Fire()
        {
            Transform closestEnemy = GetClosestEnemy();
            if (closestEnemy && Vector3.Distance(closestEnemy.transform.position, transform.position) <= Range)
            {
                Projectile projectileFired = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
                projectileFired.transform.LookAt(closestEnemy);
                projectileFired.GetComponent<Rigidbody>().linearVelocity = projectileFired.transform.forward * projectileSpeed;
                projectileFired.Init(Damage, Knockback, isPiercing);
            }
        }

        private Transform GetClosestEnemy()
        {
            Transform closest = null;
            float closestDist = Mathf.Infinity;

            foreach (GameObject enemy in enemyManager.enemies)
            {
                float dist = Vector3.Distance(enemy.transform.position, transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closest = enemy.transform;
                }
            }
            return closest;
        }
    
        public override void Upgrade(WeaponUpgradeTypes upgradeType, float amount)
        {
            switch (upgradeType)
            {
                case WeaponUpgradeTypes.damage: Damage *= amount; break;
                case WeaponUpgradeTypes.fireRate: FireRate *= amount; break;
                case WeaponUpgradeTypes.range: Range *= amount; break;
                case WeaponUpgradeTypes.knockback: Knockback *= amount; break;
                case WeaponUpgradeTypes.projectileSpeed: projectileSpeed *= amount; break;
            }
        }
    }
}
