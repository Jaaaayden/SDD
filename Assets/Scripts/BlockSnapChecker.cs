using UnityEngine;
using TMPro;

public class BlockSnapChecker : MonoBehaviour
{
    public string expectedTag; 
    public FlowchartManager puzzleManager;
    private bool isConnected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isConnected && other.CompareTag(expectedTag))
        {
            isConnected = true;
            puzzleManager.RegisterConnection();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isConnected && other.CompareTag(expectedTag)) 
        {
            Collider[] overlapping = Physics.OverlapBox(transform.position, transform.localScale / 2f, transform.rotation);

            bool stillThere = false;
            foreach (Collider col in overlapping)
            {
                if (col.CompareTag(expectedTag))
                {
                    stillThere = true;
                    break;
                }
            }

            if (!stillThere) // please work
            {
                isConnected = false;
                puzzleManager.UnregisterConnection();
            }
        }
    }
}