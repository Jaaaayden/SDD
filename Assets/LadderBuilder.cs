using UnityEngine;

public class LadderBuilder : MonoBehaviour
{
    public int requiredPlanks = 3;
    private int currentPlanks = 0;
    private bool built = false;

    public Collider[] ladderColliders; 
    public MeshRenderer[] ladderRenderers; 
    public Material solidMaterial;
    public GameObject buildZoneVisual; 

    public void CheckBuildProgress(int newPlankCount)
    {
        currentPlanks = newPlankCount;

        if (!built && currentPlanks >= requiredPlanks)
        {
            BuildLadder();
        }
    }

    private void BuildLadder()
    {
        built = true;

        foreach (var col in ladderColliders)
            col.enabled = true;

        foreach (var rend in ladderRenderers)
            rend.material = solidMaterial;

        if (buildZoneVisual != null)
            buildZoneVisual.SetActive(false);
    }
}