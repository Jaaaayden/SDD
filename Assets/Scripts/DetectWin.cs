using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DetectWin : MonoBehaviour
{
    public MarbleSpawner marbleScript;
    public AudioSource audioSource;
    public TextMeshPro winText;
    void OnTriggerStay(Collider other){
        marbleScript.DestroyMarble();
        audioSource.Play();
        winText.color = Color.green;
        winText.text = "Congratulations! You win!";
    }
    
}
