using UnityEngine;

public class SnapZone : MonoBehaviour
{
    public Transform snapPoint1;
    
    /*
    // tried to implement two snap points for the lower and upper part of the flowchart block but very buggy
    // sticking with only having lower snap point for time being

    public Transform snapPoint2;

    public Collider collider1; 
    public Collider collider2;

    public Transform GetSnapPointFromContact(Collider other) // gpt helped differentiate box colliders on the same object
    // technically bad practice because it's better to have child objects with one collider each than parent with all but too far gone
    {
        Vector3 dir;
        float dist;

        if (Physics.ComputePenetration(collider1, collider1.transform.position, collider1.transform.rotation,
                                       other, other.transform.position, other.transform.rotation,
                                       out dir, out dist))
        {
            return snapPoint1;
        }

        if (Physics.ComputePenetration(collider2, collider2.transform.position, collider2.transform.rotation,
                                       other, other.transform.position, other.transform.rotation,
                                       out dir, out dist))
        {
            return snapPoint2;
        }

        return null;
    }
    */
}