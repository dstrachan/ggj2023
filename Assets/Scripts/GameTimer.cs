using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{

    public float total_time = 60.0f;
    private float remaining_time;
    public TextMeshProUGUI timer_text;

    // Start is called before the first frame update
    void Start()
    {
        remaining_time = total_time;
    }

    // Update is called once per frame
    void Update()
    {
        if (remaining_time <= 0.0)
        {
            GameOver();
        }

        remaining_time = Mathf.Max(0.0f, remaining_time - Time.deltaTime);
        timer_text.text = remaining_time.ToString("n0");
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
    }
}
