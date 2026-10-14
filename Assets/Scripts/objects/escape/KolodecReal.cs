using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class KolodecReal : MonoBehaviour
{
    public GameObject plrcam;
    //public GameObject textE;
    public GameObject krutit;
    private bool canuse = false;
    public RotateObject RO;
    public GameObject vedro;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(
            plrcam.transform.position,
            plrcam.transform.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, 5f))
        {

            if (hit.collider.gameObject == krutit)
            {
                //Debug.Log("Попало");
                canuse = true;
                //textE.SetActive(true);
            }
            else
            {
                //Debug.Log("Не попало");
                canuse = false;
                //textE.SetActive(false);
            }
        }

        if (Input.GetKey(KeyCode.E) && canuse)
        {
            //Debug.Log("условия выполнены");
            if (vedro.transform.position.y <= 10.026)
            {
                RO.speed = -250;
                vedro.transform.position += new Vector3(0f, 0.003f, 0f);
            } else
            {
                RO.speed = 0;
            }
        } else
        {
            //Debug.Log($"{canuse}");
            RO.speed = 0;
        }
    }
}
