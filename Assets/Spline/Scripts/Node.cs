using UnityEngine;

[ExecuteAlways]
public class Node : MonoBehaviour
{
    public Transform childA, childB, up;
    Vector3 lastA, lastB;

    void Update()
    {
        if (childA == null || childB == null || up == null)
            return;

        if (childA.localPosition != lastA)
        {
            childB.localPosition = -childA.localPosition;
            lastA = childA.localPosition;
            lastB = childB.localPosition;
            return;
        }

        if (childB.localPosition != lastB)
        {
            childA.localPosition = -childB.localPosition;
            lastB = childB.localPosition;
            lastA = childA.localPosition;
            return;
        }

        up.rotation = Quaternion.LookRotation(transform.forward, up.localPosition);
    }

    void OnValidate()
    {
        if (childA != null && childB != null)
        {
            childB.localPosition = -childA.localPosition;
            lastA = childA.localPosition;
            lastB = childB.localPosition;
        }
    }
}
