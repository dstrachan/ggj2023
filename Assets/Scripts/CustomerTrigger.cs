using System;
using UnityEngine;

public class CustomerTrigger : MonoBehaviour
{
    [SerializeField] private BottleColor bottleColor;
    
    [SerializeField] private Sprite redBubble;
    [SerializeField] private Sprite blueBubble;
    [SerializeField] private Sprite greenBubble;
    [SerializeField] private Sprite pinkBubble;
    [SerializeField] private Sprite yellowBubble;
    
    [SerializeField] private Sprite redLabel;
    [SerializeField] private Sprite blueLabel;
    [SerializeField] private Sprite greenLabel;
    [SerializeField] private Sprite pinkLabel;
    [SerializeField] private Sprite yellowLabel;

    [SerializeField] private SpriteRenderer bubbleRenderer;
    [SerializeField] private SpriteRenderer labelRenderer;

    private bool wantsRootBeer = true;

    private void Start()
    {
        SetSprite();
    }

    private void SetSprite()
    {
        (bubbleRenderer.sprite, labelRenderer.sprite) = bottleColor switch
        {
            BottleColor.Red => (redBubble, redLabel),
            BottleColor.Blue => (blueBubble, blueLabel),
            BottleColor.Green => (greenBubble, greenLabel),
            BottleColor.Pink => (pinkBubble, pinkLabel),
            BottleColor.Yellow => (yellowBubble, yellowLabel),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (wantsRootBeer && col.gameObject.CompareTag("Car"))
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
                FulfillDemand();
            }
            else
            {
                print($"OH NO! INVALID BOTTLE COLOR");
            }
        }
    }

    private void FulfillDemand()
    {
        wantsRootBeer = false;
        bubbleRenderer.enabled = false;
        labelRenderer.enabled = false;
    }
}