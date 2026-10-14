using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject hinttext;
    private MetaData Looked;
    private PickupPlayer PickupPlayer;

    void Update()
    {
        Looked = null;
        PickupPlayer PickupPlayer = gameObject.GetComponent<PickupPlayer>();
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 5f))
        {
            Looked = hit.collider.GetComponentInParent<MetaData>();
            hinttext.SetActive(Looked);
        }
        else
        {
            hinttext.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E) && Looked != null &&
            Looked.InstrumentName == PickupPlayer.iteminhand)
        {
            IUsable usable = Looked.GetComponent<IUsable>();

            if (usable != null)
            {
                usable.Use();
            }
        }
    }

}

public interface IUsable
{
    void Use();
}