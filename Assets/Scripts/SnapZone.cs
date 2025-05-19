using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapZone : MonoBehaviour
{
    public GameObject snapPointObj;
    public Transform snapPoint;

    private void Awake()
    {
        snapPoint = snapPointObj.transform;
    }
    
}
