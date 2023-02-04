using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInventory : MonoBehaviour
{
    public bool IsEmpty => _bottles.Count == 0;

    private List<Bottle> _bottles;

    private void Start()
    {
        _bottles = new List<Bottle>();
    }

    public void AddBottle(BottleColor bottleColor)
    {
        _bottles.Add(new Bottle
        {
            Color = bottleColor
        });
    }
}
