using UnityEngine;
using System.Collections.Generic;

public class LadderBuilder : MonoBehaviour
{
    public Collider[] ladderColliders; 
    public MeshRenderer[] ladderRenderers; 
    public Material solidMaterial;
    public GameObject buildZoneVisual;

    public void BuildLadder()
    {
        foreach (var col in ladderColliders)
            col.enabled = true;

        foreach (var rend in ladderRenderers)
            rend.material = solidMaterial;

        Destroy(this);
    }
}