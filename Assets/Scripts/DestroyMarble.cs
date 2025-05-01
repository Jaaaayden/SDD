using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyMarble : MonoBehaviour
{
    public MarbleSpawner marbleScript;
    void OnTriggerStay(Collider other){
        marbleScript.DestroyMarble();
    }
}
