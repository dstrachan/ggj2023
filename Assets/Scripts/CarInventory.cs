using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void InventoryChangedEventHandler(List<Bottle> bottles);

public class CarInventory : MonoBehaviour
{
    public bool IsEmpty => _bottles.Count == 0;

    private Stack<Bottle> _bottles;

    public static event InventoryChangedEventHandler OnBottlesChanged;

    private void Start()
    {
        _bottles = new Stack<Bottle>();
        OnBottlesChanged?.Invoke(_bottles.ToList());
        print("You have 0 bottles");
    }

    public void AddBottle(BottleColor bottleColor)
    {
        _bottles.Push(new Bottle
        {
            Color = bottleColor
        });
        print($"You have {_bottles.Count} bottles, next is {bottleColor}");
        OnBottlesChanged?.Invoke(_bottles.ToList());
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

            OnBottlesChanged?.Invoke(_bottles.ToList());
            return true;
        }

        return false;
    }
}