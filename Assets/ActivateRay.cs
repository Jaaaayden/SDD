using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateRay : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Only the player can trigger it
        {
            LoadTheScene();
        }
        Debug.Log("Trigger Entered by: " + other.gameObject.name);
    }

    private void LoadTheScene()
    {
        SceneManager.LoadScene("TestScene");
    }
}