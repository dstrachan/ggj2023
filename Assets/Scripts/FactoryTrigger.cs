using System;
using UnityEngine;

public class FactoryTrigger : MonoBehaviour
{
    [SerializeField] private int maxBottles = 5;

    private System.Random _random;

    private static readonly int EnumCount = Enum.GetValues(typeof(BottleColor)).Length;

    private void Awake()
    {
        _random = new System.Random();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Car"))
        {
            var carInventory = col.gameObject.GetComponent<CarInventory>();
            if (!carInventory.IsEmpty)
            {
                print("CAR IS NOT EMPTY, STILL MORE TO DELIVER!");
                return;
            }

            print("LOADING CAR");
            for (var i = 0; i < maxBottles; i++)
            {
                var color = (BottleColor)_random.Next(0, EnumCount);
                carInventory.AddBottle(color);
            }
        }
    }
}