using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float damage = 3f;
    
        private Rigidbody rb;
        private Transform playerTransform;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            playerTransform = Player.Instance.transform;
        }

        private void FixedUpdate()
        {
            if (playerTransform)
            {
                Vector3 direction = (playerTransform.position - transform.position).normalized;
                Vector3 move = direction * moveSpeed * Time.deltaTime;
                rb.MovePosition(rb.position + move);
            
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 0.1f));
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            PlayerHealth playerHealth = other.collider.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
