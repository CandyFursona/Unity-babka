using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kartinupadat : MonoBehaviour
{
    public Transform player;
    public audiomanager Audiomanager;
    public NavAgent granny;
    public int number = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(
            transform.position,
            player.position
        );
        //Debug.Log($"{number}: {distance}");
        if (distance < 2.0f)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("floor"))
        {
            Audiomanager.playsound(1);
            if (granny != null && collision.contactCount > 0)
            {
                Vector3 fallPoint = collision.GetContact(0).point;

                granny.InvestigateSound(fallPoint);
            }
        }
    }
}
