using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MarbleSpawner : MonoBehaviour
{
    public GameObject marblePrefab;
    public Transform spawnPoint;
    private GameObject marbleInstance;

    public TMP_Text text;

    
    public void SpawnMarble(){
        if(marbleInstance == null){
            marbleInstance = Instantiate(marblePrefab, spawnPoint);
        }
        else{
            DestroyMarble();
            text.text = "Marble Respawned!";
            marbleInstance = Instantiate(marblePrefab, spawnPoint);
        }
    } 

    public void DestroyMarble(){
        marbleInstance.GetComponent<AudioSource>().Play();
        Destroy(marbleInstance);
        text.text = "Marble Destroyed!";
    }
}
