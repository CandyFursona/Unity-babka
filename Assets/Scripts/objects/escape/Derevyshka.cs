using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Derevyshka : MonoBehaviour, IUsable
{
    public audiomanager Audiomanager;
    public masterkey masterkey;
    public int whatis = 1;
    public void Use()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;

        if (whatis == 1)
        {
            Audiomanager.playsound(4);
            masterkey.derevo = true;
        }
        else
        {
            Audiomanager.playsound(6);
            masterkey.code = true;
        }
    }
}
