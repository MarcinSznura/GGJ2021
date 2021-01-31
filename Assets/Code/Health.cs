using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 1;
    protected int currentHealth = 1;
    public bool isInvulnerable = false;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int _damageValue)
    {
        if (isInvulnerable)
        {
            return;
        }

        currentHealth -= _damageValue;

        if (currentHealth <= 0)
        {
            die();
        }
    }

    protected virtual void die()
    {
        Destroy(gameObject);
    }
}
