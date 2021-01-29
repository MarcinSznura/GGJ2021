using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : Health
{
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

    protected void updateHealthBar()
    {
        healthBarSlider.value = currentHealth;
    }
}
