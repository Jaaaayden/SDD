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
            text.text = marbleInstance.name;
        }
    } 

    public void DestroyMarble(){
        Destroy(marbleInstance);
        marbleInstance = null;
    }
}
