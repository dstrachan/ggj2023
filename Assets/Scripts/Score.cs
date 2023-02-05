using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private int _currentBottleCount;
    private int _bottlesDelivered;
    
    private void Start()
    {
        text.text = "0";
        CarInventory.OnBottlesChanged += OnBottlesChanged;
    }

    private void OnDestroy()
    {
        CarInventory.OnBottlesChanged -= OnBottlesChanged;
    }

    private void OnBottlesChanged(List<Bottle> bottles)
    {
        if (bottles.Count < _currentBottleCount)
        {
            _bottlesDelivered += 1;
            text.text = $"{_bottlesDelivered:n0}";
        }

        _currentBottleCount = bottles.Count;
    }
}
