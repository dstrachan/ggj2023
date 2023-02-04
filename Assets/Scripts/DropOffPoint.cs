using System;
using UnityEngine;

public class DropOffPoint : MonoBehaviour
{
    [SerializeField] private BottleColor bottleColor;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Car"))
        {
            var carInventory = col.gameObject.GetComponent<CarInventory>();
            if (carInventory.IsEmpty)
            {
                print("CAR IS EMPTY, GO TO THE FACTORY FOR MORE BOTTLES!");
                return;
            }

            print("UNLOADING CAR");
            if (carInventory.TryRemoveBottle(bottleColor))
            {
                print($"UNLOADED {bottleColor} BOTTLE");
            }
            else
            {
                print($"OH NO! INVALID BOTTLE COLOR");
            }
        }
    }
}