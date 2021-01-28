using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyHealth _health))
        {
            _health.TakeDamage(1);
        }
    }
}
