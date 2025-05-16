using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MarbleSpawner : MonoBehaviour
{
    public GameObject marblePrefab;
    public Transform spawnPoint;
    public AudioClip soundClip;
    private GameObject marbleInstance;

    public TMP_Text text;

    
    public void SpawnMarble(){
        if(marbleInstance == null){
            marbleInstance = Instantiate(marblePrefab, spawnPoint);
            text.text = "Marble Respawned!";
        }
        else{
            DestroyMarble();
            text.text = "Marble Respawned!";
            marbleInstance = Instantiate(marblePrefab, spawnPoint);
        }
    } 

    public void DestroyMarble(){
        AudioSource audioSource = marbleInstance.GetComponent<AudioSource>();
        AudioSource.PlayClipAtPoint(soundClip, marbleInstance.transform.position);
        Destroy(marbleInstance);    
        text.text = "Marble Destroyed!";
    }
}
