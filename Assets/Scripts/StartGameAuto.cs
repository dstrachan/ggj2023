using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartGameAuto : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadSceneAsync("MainGame");
    }
}
