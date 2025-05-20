using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MarbleDestroyer : MonoBehaviour
{
    public MarbleSpawner marbleScript;
    void OnTriggerStay(Collider other){
        marbleScript.DestroyMarble();
    }
}
