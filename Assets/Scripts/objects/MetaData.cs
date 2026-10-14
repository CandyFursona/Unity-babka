using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaData : MonoBehaviour
{
    public string InstrumentName; // для использования

    [HideInInspector] public Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
}
