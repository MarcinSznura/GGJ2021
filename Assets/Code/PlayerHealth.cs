using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public System.Action OnPlayerDeath = null;

    protected override void Start()
    {
        currentHealth = PlayerPersistantStats.Instance.PlayerPreviousHealth;

        if (currentHealth == 0)
        {
            currentHealth = maxHealth;
        }

        FindObjectOfType<GameplayUI>().UpdateUI(currentHealth, maxHealth);
    }

    public override void TakeDamage(int _damageValue)
    {
        base.TakeDamage(_damageValue);
        FindObjectOfType<GameplayUI>().UpdateUI(currentHealth, maxHealth);
    }

    protected override void die()
    {
        OnPlayerDeath?.Invoke();
        gameObject.SetActive(false);
    }
}
