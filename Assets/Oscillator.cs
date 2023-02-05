using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector2 originalPos;
    public float amplitude = 1.0f;
    public float period = 3.0f;

    private void Start()
    {
        originalPos = new Vector2(transform.localPosition.x, transform.localPosition.y);
    }


    // Update is called once per frame
    void Update()
    {
        var offset = new Vector2(0.0f, 1.0f) * Mathf.Sin(period * Time.time) * amplitude;
        transform.localPosition = offset + originalPos;
    }
}
