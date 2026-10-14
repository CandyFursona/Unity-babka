using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickupItem : MonoBehaviour
{
    public Vector3 holdLocalPos = new Vector3(0.993f, -0.54f, 0.951f);
    public Vector3 holdLocalRot = Vector3.zero;
    public string ObjName;

    [HideInInspector] public Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
}