using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class NormalizedProgressBar : MonoBehaviour
{
    [SerializeField] private Gradient _gradient;

    protected Slider _slider;
    private Image _fillArea;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _fillArea = _slider.fillRect.GetComponent<Image>();
    }

    private void OnEnable()
    {
        OnEnabling();

        _slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnDisable()
    {
        OnDisabling();

        _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    protected virtual void OnEnabling() { }
    protected virtual void OnDisabling() { }

    private void OnSliderValueChanged(float newValue)
    {
        _fillArea.color = _gradient.Evaluate(newValue);
    }

    protected void UpdateSliderValue(float value)
    {
        _slider.value = value;
    }
}
