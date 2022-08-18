using FireBalls3D.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : NormalizedProgressBar
{
    private Health _health;

    public void Init(Health health)
    {
        _health = health;

        UpdateSliderValue(_health.Value);

        enabled = true;
    }

    protected override void OnEnabling()
    {
        _health.Damaged += OnHealthDamaged;
    }

    protected override void OnDisabling()
    {
        _health.Damaged -= OnHealthDamaged;
    }

    private void OnHealthDamaged()
    {
        UpdateSliderValue((float)_health.Value / (float)Config.TankHealth);
    }
}
