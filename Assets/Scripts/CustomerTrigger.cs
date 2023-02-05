using System;
using UnityEngine;
using UnityEngine.VFX;

public class CustomerTrigger : MonoBehaviour
{
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

    [SerializeField] private VisualEffect fireworks;
    [SerializeField] private AudioSource successSound;

    private bool _wantsRootBeer = true;
    private BottleColor _bottleColor;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_wantsRootBeer && col.gameObject.CompareTag("Car"))
        {
            var carInventory = col.gameObject.GetComponent<CarInventory>();
            if (carInventory.IsEmpty)
            {
                print("CAR IS EMPTY, GO TO THE FACTORY FOR MORE BOTTLES!");
                return;
            }

            print("UNLOADING CAR");
            if (carInventory.TryRemoveBottle(_bottleColor))
            {
                print($"UNLOADED {_bottleColor} BOTTLE");
                FulfillDemand();
            }
            else
            {
                print($"OH NO! INVALID BOTTLE COLOR");
            }
        }
    }

    public void SetDemand(BottleColor bottleColor)
    {
        _bottleColor = bottleColor;
        
    
        var effectColor = bottleColor switch
        {
            BottleColor.Red => new Vector4(5,0,0,0),
            BottleColor.Blue => new Vector4(0,0,5,0),
            BottleColor.Green => new Vector4(0,5,0,0),
            BottleColor.Pink => new Vector4(5,0,5,0),
            BottleColor.Yellow => new Vector4(5,5,0,0),
            _ => throw new ArgumentOutOfRangeException()
        };
        
        fireworks.SetVector4("MainColor", effectColor);

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

    private void FulfillDemand()
    {
        _wantsRootBeer = false;
        bubbleRenderer.enabled = false;
        labelRenderer.enabled = false;
        
        successSound.PlayOneShot(successSound.clip);
        fireworks.Play();
    }
}