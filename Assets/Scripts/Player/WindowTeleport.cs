using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class WindowTeleport : MonoBehaviour
{
    [Header("Hitboxes")]
    public GameObject Windowisdoma;
    public GameObject Windowisulici;

    [Header("Points")]
    public GameObject voiddoma;
    public GameObject voidulica;

    [Header("Other")]
    public GameObject plrcam;
    public GameObject Player;
    public GameObject textE;

    public bool isLookingAtWindow1 = false;
    public bool isLookingAtWindow2 = false;


    void Update()
    {
        Ray ray = new Ray(
            plrcam.transform.position,
            plrcam.transform.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, 5f))
        {
            //Debug.Log("Попадание: " + hit.collider.name);

            if (hit.collider.gameObject == Windowisdoma)
            {
                isLookingAtWindow1 = true;
                isLookingAtWindow2 = false;
                textE.SetActive(true);
            }
            else if (hit.collider.gameObject == Windowisulici)
            {
                isLookingAtWindow2 = true;
                isLookingAtWindow1 = false;
                textE.SetActive(true);
            }
            else
            {
                isLookingAtWindow2 = false;
                isLookingAtWindow1 = false;
                textE.SetActive(false);
            }
        }
        else
        {
            isLookingAtWindow2 = false;
            isLookingAtWindow1 = false;
            textE.SetActive(false);
        }
        

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isLookingAtWindow1)
            {
                Player.GetComponent<CharacterController>().enabled = false;
                Player.GetComponent<PlayerController>().enabled = false;
                Player.transform.position = voidulica.transform.position;
                Player.GetComponent<CharacterController>().enabled = true;
                Player.GetComponent<PlayerController>().enabled = true;
            }
            else if (isLookingAtWindow2)
            {
                Player.GetComponent<CharacterController>().enabled = false;
                Player.GetComponent<PlayerController>().enabled = false;
                Player.transform.position = voiddoma.transform.position;
                Player.GetComponent<CharacterController>().enabled = true;
                Player.GetComponent<PlayerController>().enabled = true;
            }
        }
    }
}