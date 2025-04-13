using UnityEngine;
using System.Collections.Generic;
using TMPro; 

public class BuildZone : MonoBehaviour
{
    public TextMeshProUGUI buildZoneText; 
    public LadderBuilder ladderBuilder;
    private int plankCount = 0;
    private HashSet<GameObject> enteredPlanks = new HashSet<GameObject>();
    
    private void Start()
    {
        UpdateBuildZoneText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plank") && !enteredPlanks.Contains(other.gameObject))
        {
            enteredPlanks.Add(other.gameObject);
            plankCount++;
            UpdateBuildZoneText();
            ladderBuilder.CheckBuildProgress(plankCount);
        }
    }

    private void UpdateBuildZoneText()
    {
        buildZoneText.text = "Planks dropped: " + plankCount + "/3";
    }
}