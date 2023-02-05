using System;
using UnityEngine;
using UnityEngine.UI;

public class BottleUI : MonoBehaviour
{
    public Image bottle;
    public Image bottleGlow;
    public Sprite redBottle;
    public Sprite blueBottle;
    public Sprite greenBottle;
    public Sprite pinkBottle;
    public Sprite yellowBottle;

    public void SetColour(BottleColor bc)
    {
        bottle.sprite = bc switch
        {
            BottleColor.Red => redBottle,
            BottleColor.Blue => blueBottle,
            BottleColor.Green => greenBottle,
            BottleColor.Pink => pinkBottle,
            BottleColor.Yellow => yellowBottle,
            _ => throw new ArgumentOutOfRangeException(nameof(bc), bc, null)
        };
    }

    public void HideBottle()
    {
        bottle.enabled = false;
        bottleGlow.enabled = false;
    }

    public void ShowBottle()
    {
        bottle.enabled = true;
    }

    public void SetHighlight(bool highlight)
    {
        bottleGlow.enabled = highlight;
    }
}