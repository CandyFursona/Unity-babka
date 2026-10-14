using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fall : MonoBehaviour
{
    public audiomanager Audiomanager;
    public NavAgent granny;

    void Start()
    {
        int playerLayer = LayerMask.NameToLayer("player");
        int babkaLayer = LayerMask.NameToLayer("babka");

        GameObject[] objects =
            FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (GameObject obj in objects)
        {
            if (obj.layer == playerLayer)
            {
                audiomanager audio =
                    obj.GetComponent<audiomanager>();

                if (audio == null)
                    audio = obj.GetComponentInChildren<audiomanager>();

                if (audio != null)
                {
                    Audiomanager = audio;
                    Debug.Log("Audiomanager найден: " + obj.name);
                }
            }

            if (obj.layer == babkaLayer)
            {
                NavAgent agent = obj.GetComponent<NavAgent>();

                if (agent == null)
                    agent = obj.GetComponentInChildren<NavAgent>();

                if (agent != null)
                {
                    granny = agent;
                    Debug.Log("NavAgent найден: " + obj.name);
                }
            }
        }

        if (playerLayer == -1)
            Debug.LogError("Слой player не существует!");

        if (babkaLayer == -1)
            Debug.LogError("Слой babka не существует!");

        if (Audiomanager == null)
            Debug.LogError("Audiomanager не найден!");

        if (granny == null)
            Debug.LogError("NavAgent не найден!");
    }
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

            if (granny != null && collision.contactCount > 0)
            {
                Vector3 fallPoint = collision.GetContact(0).point;
                fallPoint.y += 3f;

                granny.InvestigateSound(fallPoint);
            }
        }
    }
}
