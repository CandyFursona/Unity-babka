using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DoorBoom : MonoBehaviour
{
    public GameObject babka;
    public GameObject babkafull;
    public audiomanager soundmanager;
    public bool nevibita = true;

    void Start()
    {

    }

    IEnumerator BoomDoor()
    {
        if (nevibita)
        {
            nevibita = false;
            yield return new WaitForSeconds(3f);
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.transform.position = new Vector3(-2.602f, 10.26f, -12.07f);
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * 400f);
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 60f);
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * 600f);
            soundmanager.playsound(8);
            babkafull.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            babkafull.GetComponent<NavAgent>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, babka.transform.position);
        //Debug.Log(distance);
        if (distance <= 3)
        {
            if (nevibita)
            {
                Debug.Log("выбиваем дверь");
                babkafull.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                babkafull.GetComponent<NavAgent>().enabled = false;
                StartCoroutine(BoomDoor());
            }
        }
    }
}
