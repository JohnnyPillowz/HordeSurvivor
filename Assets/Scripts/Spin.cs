using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] public float spinSpeed = 10f;

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, spinSpeed * Time.fixedDeltaTime);
    }
}
