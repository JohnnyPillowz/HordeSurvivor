
using System;
using UnityEditor.Timeline;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage;
    private float knockback;
    private bool isPiercing;
    
    private Player player;

    private void Start()
    {
        player = Player.Instance;
    }

    public void Init(float damage, float knockback, bool isPiercing)
    {
        this.damage = damage;
        this.isPiercing = isPiercing;
        this.knockback = knockback;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (!enemyHealth) { return; }

        enemyHealth.TakeDamage(damage);
        
        Rigidbody otherRB = other.GetComponent<Rigidbody>();
        if (otherRB)
        {
            Vector3 knockbackDir = (other.transform.position - player.transform.position).normalized;
            otherRB.linearVelocity = knockbackDir * knockback;
        }

        if (!isPiercing)
        {
            Destroy(this.gameObject);
        }
    }
}
