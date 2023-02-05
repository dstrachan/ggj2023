using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float startingTime = 60f;
    [SerializeField] private float secondsPerRefill = 20f;
    private float remaining_time;
    public TextMeshProUGUI timer_text;

    public GameObject gameOverMenu;
    public GameObject gameOverBackground;
    public GameObject factoryPointer;

    // Start is called before the first frame update
    void Start()
    {
        remaining_time = startingTime;
        CarInventory.OnBottlesChanged += OnBottlesChanged;
    }
    
    private void OnDestroy()
    {
        CarInventory.OnBottlesChanged -= OnBottlesChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if (remaining_time <= 0.0)
        {
            GameOver();
        }

        remaining_time = Mathf.Max(0.0f, remaining_time - Time.deltaTime);
        timer_text.text = Mathf.Ceil(remaining_time).ToString("n0");
    }

    void GameOver()
    {
        gameOverBackground.SetActive(true);
        gameOverMenu.SetActive(true);

        factoryPointer.SetActive(false);

        enabled = false;
    }

    private void OnBottlesChanged(List<Bottle> bottles)
    {
        if (bottles.Count != 5) return;
        remaining_time += secondsPerRefill;
    }
}