using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottleUI : MonoBehaviour
{
    public Image bottle;
    public Image bottleGlow;
    public Sprite redBottle;
    public Sprite greenBottle;
    public Sprite blueBottle;

    // Start is called before the first frame update
    void Start()
    {
        SetColour(BottleColor.Blue);
    }


    public void SetColour(BottleColor bc)
    {
        bottle.sprite = bc switch
        {
            BottleColor.Green => greenBottle,
            BottleColor.Red => redBottle,
            BottleColor.Blue => blueBottle
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
