using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineTester : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private lineController line;

    private void Start(){
        line.SetUpLine(points);
    }
   
    
}
