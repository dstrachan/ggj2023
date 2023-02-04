using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateUI : MonoBehaviour
{

    public CarInventory carInventory;
    public List<BottleUI> bottles;

    // Start is called before the first frame update
    void Start()
    {       
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var bottle in bottles)
        {
            bottle.SetColour(BottleColor.Blue);
        }
    }
}
