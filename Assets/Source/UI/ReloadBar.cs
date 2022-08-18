using FireBalls3D.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadBar : NormalizedProgressBar
{
    private ITimer _gunCooldown;

    public void Init(ITimer gunCooldown)
    {
        _gunCooldown = gunCooldown;

        UpdateSliderValue(0);

        enabled = true;
    }

    protected override void OnEnabling()
    {
        _gunCooldown.Updated += OnGunCooldownUpdated;
    }

    protected override void OnDisabling()
    {
        _gunCooldown.Updated -= OnGunCooldownUpdated;
    }

    private void OnGunCooldownUpdated(float accumulatedTime)
    {
        UpdateSliderValue(accumulatedTime / Config.GunReload);
    }
}
