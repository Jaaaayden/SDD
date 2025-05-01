using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleSpawner : MonoBehaviour
{
    public GameObject marblePrefab;
    public Transform spawnPoint;
    private GameObject marbleInstance;

    
    public void SpawnMarble(){
        if(marbleInstance == null){
            marbleInstance = Instantiate(marblePrefab, spawnPoint);
        }
    } 

    public void DestroyMarble(){
        Destroy(marbleInstance);
    }
}
