using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public enum DamageType
    {
        Attack = 0,
        Special = 1
    }

    [SerializeField] DamageType damageType = DamageType.Attack;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger " + other.name);
        if (other.TryGetComponent(out EnemyHealth _health))
        {
            int _damage = damageType == DamageType.Attack ? PlayerAttack.Instance.AttackDamage : PlayerAttack.Instance.SpecialAttackDamage;
            _health.TakeDamage(_damage);
        }
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision " + collision.gameObject.name);
        if (collision.gameObject.TryGetComponent(out EnemyHealth _health))
        {
            int _damage = damageType == DamageType.Attack ? PlayerAttack.Instance.AttackDamage : PlayerAttack.Instance.SpecialAttackDamage;
            _health.TakeDamage(_damage);
        }
    }
    */
}
