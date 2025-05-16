using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class krispr : MonoBehaviour
{
    [Tooltip("crisp")]
    public string targetSceneName;

    private void OnTriggerEnter(Collider other)
    {
        // You can tag your VR player or controller as "Player" for filtering
        SceneManager.LoadScene(targetSceneName);
    }
}
