using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guilotine : MonoBehaviour, IUsable
{
    public GameObject MelonModel;
    public PickupPlayer plrctrl;

    public bool meloninguo = false;
    void Start()
    {

    }

    public void Use()
    {
        MelonModel.SetActive(true);
        plrctrl.DestroyItemInHand();
        meloninguo = true;
    }
}
