using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineController : MonoBehaviour //Script based on https://www.youtube.com/watch?v=5ZBynjAsfwI&t=21s
{   private LineRenderer lr;
    private Transform[] points;

    private void Awake(){
        lr = GetComponent<LineRenderer>();
    }
    public void SetUpLine(Transform[] points){
        lr.positionCount = points.Length;
        this.points = points;
    }

    // Update is called once per frame
    void Update(){
        for (int i = 0; i<points.Length; i++){
            lr.SetPosition(i, points[i].position);
        }
    }
}
