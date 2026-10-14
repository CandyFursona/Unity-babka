using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class kolodec : MonoBehaviour, IUsable
{
    public PickupPlayer plrctrl;
    public GameObject VisualWinch;
    void Start()
    {
        
    }

    public void Use()
    {
        plrctrl.DestroyItemInHand();
        VisualWinch.SetActive(true);
        gameObject.GetComponent<kolodec>().enabled = false;
    }
}
