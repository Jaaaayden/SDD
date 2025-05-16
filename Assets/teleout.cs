using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class teleout : MonoBehaviour
{
    [Tooltip("Goodout")]
    public string targetSceneName;
    public TextMeshProUGUI textshii;

    private void OnTriggerEnter(Collider other)
    {
        // You can tag your VR player or controller as "Player" for filtering
        textshii.text = "Welcome back... Wait!! The first thing to do in the lab is put on safety goggles!";
        SceneManager.LoadScene(targetSceneName);
    }
}
