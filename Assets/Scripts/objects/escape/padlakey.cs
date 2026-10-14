using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class padlakey : MonoBehaviour, IUsable
{
    public audiomanager Audiomanager;
    public masterkey masterkey;
    public GameObject piza;
    public void Use()
    {
        Audiomanager.playsound(6);
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;

        Rigidbody rb2 = piza.GetComponent<Rigidbody>();
        rb2.isKinematic = false;
        rb2.velocity = Vector3.zero;
        masterkey.padla_key = true;
    }
}
