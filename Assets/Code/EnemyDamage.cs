using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int damageValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger " + other.name);
        if (other.TryGetComponent(out PlayerHealth _health))
        {
            _health.TakeDamage(damageValue);
        }
    }
}
