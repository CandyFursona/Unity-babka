using UnityEngine;

public class StreetDetector : MonoBehaviour
{
    public Transform player;
    public Collider streetZone1;
    public Collider streetZone2;

    public bool isInStreet;

    void Update()
    {
        isInStreet =
            IsInside(player.position, streetZone1) ||
            IsInside(player.position, streetZone2);
    }

    bool IsInside(Vector3 point, Collider zone)
    {
        return zone.bounds.Contains(point);
    }
}