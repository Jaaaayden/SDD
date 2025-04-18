using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class BuildZone : MonoBehaviour
{
    public TextMeshProUGUI buildZoneText;
    public TextMeshProUGUI ladderBuiltText;
    public List<LadderBuilder> ladderBuilders; 
    public List<Collider> climbZones;
    private int currentPlanks = 0;
    private int builtLadders = 0;
    public int totalLaddersToBuild = 10;
    public GameObject buildZoneVisual;
    private HashSet<GameObject> enteredPlanks = new HashSet<GameObject>();
    public AudioClip soundClip3;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateBuildZoneText(0);
        UpdateLadderText();
        ActivateNextLadder();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Plank") || enteredPlanks.Contains(other.gameObject)) return;

        enteredPlanks.Add(other.gameObject);
        currentPlanks++;
        UpdateBuildZoneText(currentPlanks);

        if (currentPlanks >= 3)
        {
            if (soundClip3 != null)
            {
                AudioSource.PlayClipAtPoint(soundClip3, transform.position);
            }
            StartCoroutine(BuildLadderWithDelay(3f)); 
        }
    }
    
    IEnumerator BuildLadderWithDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);

        ladderBuilders[builtLadders].BuildLadder();
        ActivateClimbZone();
        builtLadders++;

        foreach (GameObject plank in enteredPlanks)
        {
            Destroy(plank);
        }

        enteredPlanks.Clear();
        currentPlanks = 0;
        UpdateLadderText();

        if (builtLadders >= totalLaddersToBuild)
        {
            if (buildZoneVisual != null)
                buildZoneVisual.SetActive(false);

            gameObject.SetActive(false); 
        }
        else
        {
            ActivateNextLadder();
            UpdateBuildZoneText(0);
        }
    }

    private void ActivateNextLadder()
    {
        if (builtLadders < ladderBuilders.Count)
        {
            LadderBuilder nextLadder = ladderBuilders[builtLadders];

            nextLadder.gameObject.SetActive(true);
        }
    }

    private void ActivateClimbZone() // this is called before builtLadders is incremented
    {
        Collider collider = climbZones[builtLadders];

        collider.enabled = true;
    }

    public void UpdateBuildZoneText(int currentPlanks)
    {
        buildZoneText.text = "Planks dropped: " + currentPlanks + "/3";
    }

    public void UpdateLadderText()
    {
        ladderBuiltText.text = "Ladders built: " + builtLadders + "/10";
    }
}