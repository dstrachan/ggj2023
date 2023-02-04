using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInventory : MonoBehaviour
{
    public bool IsEmpty => _bottles.Count == 0;

    private Stack<Bottle> _bottles;

    private void Start()
    {
        _bottles = new Stack<Bottle>();
        print("You have 0 bottles");
    }

    public void AddBottle(BottleColor bottleColor)
    {
        _bottles.Push(new Bottle
        {
            Color = bottleColor
        });
        print($"You have {_bottles.Count} bottles, next is {bottleColor}");
    }

    public bool TryRemoveBottle(BottleColor bottleColor)
    {
        if (_bottles.Count > 0 && _bottles.Peek().Color == bottleColor)
        {
            _bottles.Pop();
            if (_bottles.Count > 0)
            {
                print($"You have {_bottles.Count} bottles, next is {_bottles.Peek().Color}");
            }
            else
            {
                print($"You have 0 bottles, go to the factory to get more!");
            }

            return true;
        }

        return false;
    }
}
