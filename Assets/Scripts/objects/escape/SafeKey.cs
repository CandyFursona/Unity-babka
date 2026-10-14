using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeKey : MonoBehaviour, IUsable
{
    public audiomanager soundmanager;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Use()
    {
        soundmanager.playsound(10);
        Destroy(gameObject);
    }
}
