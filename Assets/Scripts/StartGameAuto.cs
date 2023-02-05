using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartGameAuto : MonoBehaviour
{
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync("MainGame");
    }

    private void Start()
    {
        StartCoroutine(Wait());
    }
}
