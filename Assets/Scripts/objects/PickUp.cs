using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

// доисторический скрипт, он не используется где либо

/*
public class PickUp : MonoBehaviour
{
public GameObject tochka;
public GameObject voidphase;
public GameObject textE;
public GameObject Window;
public Transform Player;
private bool cmotrit;
private bool itemhold = false;
public GameObject Hint;

public Camera playerCamera;  
public float distance = 5f;
public void UIText(int state)
{
    if (state == 1)
    {
        cmotrit = true;
        tochka.SetActive(false);
        textE.SetActive(true);
    }
    else
    {
        cmotrit = false;
        tochka.SetActive(true);
        textE.SetActive(false);
    }
}

public void PickUpObj()
{
    Rigidbody rb = GetComponent<Rigidbody>();
    rb.isKinematic = true;
    itemhold = true;
    gameObject.transform.SetParent(Player);
    gameObject.transform.localPosition = new Vector3(0.993f, -0.54f, 0.951f);
}

public void DropObj()
{
    gameObject.transform.localPosition = playerCamera.transform.localPosition; 
    gameObject.transform.SetParent(null); 
    itemhold = false; 

    Rigidbody rb = GetComponent<Rigidbody>(); 
    rb.isKinematic = false; 
    rb.AddForce(playerCamera.transform.forward * 3f, ForceMode.Impulse); 

}

void Update()
{
    Hint.SetActive(itemhold);
    voidphase.SetActive(itemhold);
    // НЕ ТРОГАТЬ, Я НЕ ЕБУ КАК ОНО РАБОТАЕТ
    Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit, distance))
    {
        if (hit.collider.gameObject == gameObject)
        {
            UIText(1);
        }
        else
        {
            UIText(0);
        }
        if (hit.collider.gameObject == Window)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

            }
        }
    }
    else
    {
        UIText(0);
    }
    if (Input.GetKeyDown(KeyCode.E) && cmotrit && !voidphase.activeSelf)
    {
        PickUpObj();
    }
    if (Input.GetKeyDown(KeyCode.Space) && itemhold)
    {
        DropObj();
    }

}
}
*/