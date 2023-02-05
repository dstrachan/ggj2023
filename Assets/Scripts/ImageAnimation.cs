using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageAnimation : MonoBehaviour {

    public Sprite[] sprites;
    public float frameTimeSeconds = 6;
    public bool loop = true;
    public bool destroyOnEnd = false;

    private float lastFrame;
    
    private int index = 0;
    private Image image;
    private int frame = 0;

    void Awake() {
        image = GetComponent<Image> ();
        lastFrame = Time.time;
    }

    void Update() 
    {
        if (!loop && index == sprites.Length)
        {
            return;
        }

        if (lastFrame + frameTimeSeconds < Time.time)
        {
            image.sprite = sprites[index];
            lastFrame = Time.time;
            index++;
            if (index >= sprites.Length)
            {
                if (loop) index = 0;
                if (destroyOnEnd) Destroy(gameObject);
            }
        }
    }
}