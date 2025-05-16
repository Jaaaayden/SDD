using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
using TMPro;

public class removedgene : MonoBehaviour
{
    // Start is called before the first frame update
    [Tooltip("endgame")]
    public string targetSceneName;

    private XRGrabInteractable grabInteractable;
    private static int coun = 0;
    
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(gab);
    }

    // Update is called once per frame
    private void gab(SelectEnterEventArgs args){
        gameObject.SetActive(false);
        coun++;
        if (coun == 4){
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
