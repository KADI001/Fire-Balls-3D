using FireBalls3D.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreText : MonoBehaviour
{
    private Score _score;
    private TextMeshProUGUI _textMesh;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void Init(Score score)
    {
        _score = score;

        enabled = true;
    }

    private void OnEnable()
    {
        _score.Changed += OnScoreChanged;
    }

    private void OnDisable()
    {
        _score.Changed -= OnScoreChanged;
    }

    private void OnScoreChanged()
    {
        _textMesh.text = _score.Value.ToString();
    }
}
