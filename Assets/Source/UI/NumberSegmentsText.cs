using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FireBalls3D.Model;
using System.Linq;
using System;

[RequireComponent(typeof(TextMeshProUGUI))]
public class NumberSegmentsText : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;
    private Pipe _pipe;

    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void Init(Pipe pipe)
    {
        _pipe = pipe;
        _textMeshPro.text = _pipe.NumberSegments.ToString();
        enabled = true;
    }

    private void OnEnable()
    {
        _pipe.SegmentDestroyed += OnSegmentDestroyed;
    }

    private void OnDisable()
    {
        _pipe.SegmentDestroyed -= OnSegmentDestroyed;
    }

    private void OnSegmentDestroyed()
    {
        _textMeshPro.text = _pipe.NumberSegments.ToString();
    }
}
