using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
using TMPro;

public class samples : MonoBehaviour
{   
    [Tooltip("Biotech")]
    public string targetSceneName;
    public TextMeshProUGUI textshii;
    private XRGrabInteractable grabInteractable;

    // Make count static so it's shared across all instances
    //private static int globalCount = 0;

    //private static bool sceneLoaded = false;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(Grabbed);
    }

    public void Grabbed(SelectEnterEventArgs args)
    {
        gameObject.SetActive(false);
      //  globalCount++;
        SceneManager.LoadScene(targetSceneName);
        textshii.text = "Welcome back... Wait!! The first thing to do in the lab is put on safety goggles!";

        //if (globalCount == 3 && !sceneLoaded)
       // {
           // sceneLoaded = true;
           // SceneManager.LoadScene(targetSceneName);
        //}
    }
}
