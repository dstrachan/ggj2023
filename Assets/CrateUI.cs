using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CrateUI : MonoBehaviour
{

    public CarInventory carInventory;
    public List<BottleUI> bottleUIs;
    public AudioSource checkForRefillAudio;
    public GameObject refillSign;
    

    private bool once;
    // Start is called before the first frame update
    void Start()
    {
     
        CarInventory.OnBottlesChanged += UpdateBottleUI;
        CarInventory.OnBottlesChanged += CheckForRefills;
    }

    private void OnDestroy()
    {
        CarInventory.OnBottlesChanged -= UpdateBottleUI;
        CarInventory.OnBottlesChanged -= CheckForRefills;
    }

    private void UpdateBottleUI(List<Bottle> bottles)
    {
        var totalBottles = bottles.Count;
        int offset = bottleUIs.Count - bottles.Count;

        for (int i = 0; i < offset; i++)
        {
            bottleUIs[i].HideBottle();
        }

        for (int i = offset; i < bottleUIs.Count; i++)
        {
            bottleUIs[i].ShowBottle();
            bottleUIs[i].SetColour(bottles[i - offset].Color);
            bottleUIs[i].SetHighlight(false);
        }

        if (bottles.Count > 0)
        {
            bottleUIs[offset].SetHighlight(true);
        }


    }

    private void CheckForRefills(List<Bottle> bottles)
    {
        if (!once)
        {
            once = true;
            return;
        }
        
        if(bottles.Count == 0)
        {
            checkForRefillAudio.Play();
            refillSign.SetActive(true);
        }
        else
        {
            refillSign.SetActive(false);
        }
    }

}
