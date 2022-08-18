using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuContent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _background;

    public float Alpha => 0.8f;

    public void SetColorAlpha(float alpha)
    {
        float clampedAlpha = Mathf.Clamp(alpha, 0, 1);

        _text.color = GetColorWithNewAlpha(_text.color, clampedAlpha);
        _background.color = GetColorWithNewAlpha(_background.color, clampedAlpha);
    }

    private Color GetColorWithNewAlpha(Color color, float alpha)
    {
        Color newColor = color;
        newColor.a = alpha;

        return newColor;
    }
}
