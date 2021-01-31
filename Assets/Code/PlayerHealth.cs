using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    public System.Action OnPlayerDeath = null;

    GameplayUI UI = null;

    protected override void Start()
    {
        currentHealth = PlayerPersistantStats.Instance.PlayerPreviousHealth;

        if (currentHealth == 0)
        {
            currentHealth = maxHealth;
        }

        UI = FindObjectOfType<GameplayUI>();
        UI.UpdateUI(currentHealth, maxHealth);
    }

    public override void TakeDamage(int _damageValue)
    {
        base.TakeDamage(_damageValue);
        UI.UpdateUI(currentHealth, maxHealth);
    }

    protected override void die()
    {
        UI.UpdateUI(currentHealth, maxHealth);

        OnPlayerDeath?.Invoke();
        UI.StartLoadingMainMenu();
        gameObject.SetActive(false);
    }
}
