using UnityEngine;
using TMPro;

public class FlowchartManager : MonoBehaviour
{
    public TextMeshProUGUI statusText; 
    public Transform xrRig;          
    private int totalConnectionsNeeded = 6;
    private int currentConnections = 0;

    private void Start()
    {
        UpdateStatus();
    }

    public void RegisterConnection()
    {
        currentConnections++;
        UpdateStatus();

        if (currentConnections >= totalConnectionsNeeded)
        {
            PuzzleComplete();
        }
    }

    public void UnregisterConnection()
    {
        currentConnections = Mathf.Max(0, currentConnections - 1);
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        if (statusText != null)
        {
            statusText.text = $"Connections: {currentConnections}/{totalConnectionsNeeded}";
        }
    }

    private void PuzzleComplete()
    {
        if (xrRig != null)
        {
            xrRig.position = Vector3.zero;  
        }
    }

    public void SetTotalConnections(int total)
    {
        totalConnectionsNeeded = total;
    }
}