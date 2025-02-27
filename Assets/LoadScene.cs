using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class LoadScene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        LoadTheScene();
    }

    private void LoadTheScene()
    {
        SceneManager.LoadScene(1);
    }
}