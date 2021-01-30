using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : Health
{
    public System.Action<EnemyHealth> OnEnemyDeath = null;

    [SerializeField] Slider healthBarSlider = null;

    protected override void Start()
    {
        base.Start();

        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;
    }

    public override void TakeDamage(int _damageValue)
    {
        base.TakeDamage(_damageValue);

        updateHealthBar();
    }

    protected override void die()
    {
        base.die();

        OnEnemyDeath?.Invoke(this);
    }

    protected void updateHealthBar()
    {
        healthBarSlider.value = currentHealth;
    }
}
