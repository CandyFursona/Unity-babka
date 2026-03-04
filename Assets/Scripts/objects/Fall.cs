using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fall : MonoBehaviour
{
    public audiomanager Audiomanager;
    public NavAgent granny;
    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb.velocity.y < -10)
        {
            rb.velocity = new Vector3(
                rb.velocity.x,
                -10,
                rb.velocity.z
            );
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("floor"))
        {
            Audiomanager.playsound(1);
        }
    }
}
