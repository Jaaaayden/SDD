using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DetectWin : MonoBehaviour
{
    public MarbleSpawner marbleScript;
    public AudioSource audioSource;
    public TextMeshPro winText;
    public AudioClip soundClip;
    public GameObject bucket;
    void OnTriggerStay(Collider other){
        marbleScript.DestroyMarble();
        AudioSource.PlayClipAtPoint(soundClip, bucket.transform.position);
        winText.color = Color.green;
        winText.text = "Congratulations! You win!";
    }
    
}
