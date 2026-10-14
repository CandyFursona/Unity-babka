
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class connectObjects : MonoBehaviour
{
    public Transform objectA;
    public Transform objectB;

    private LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.useWorldSpace = true;
    }

    void Update()
    {
        if (objectA == null || objectB == null)
            return;

        line.SetPosition(0, objectA.position);
        line.SetPosition(1, objectB.position);
    }
}
