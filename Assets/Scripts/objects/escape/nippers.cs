using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nippers : MonoBehaviour, IUsable
{
    public audiomanager Audiomanager;
    public masterkey masterkey;
    public GameObject redobj;
    public GameObject redobj2;
    public GameObject gameObject2;
    public Material greenmaterial;
    public Material slomaniuprovod;
    public int type = 1;
    public void Use()
    {
        Debug.Log($"Use() type={type} Audiomanager={Audiomanager} masterkey={masterkey} redobj={redobj} redobj2={redobj2} gameObject2={gameObject2}");

        if (Audiomanager == null) { Debug.LogError("Audiomanager НЕ назначен!"); return; }
        if (masterkey == null) { Debug.LogError("masterkey НЕ назначен!"); return; }

        if (type == 1)
        {
            if (redobj == null) { Debug.LogError("redobj НЕ назначен!"); return; }

            Audiomanager.playsound(7);

            var r = GetComponent<Renderer>();
            if (r == null) { Debug.LogError("Нет Renderer на объекте nippers!"); return; }

            r.material = slomaniuprovod;

            var rr = redobj.GetComponent<Renderer>();
            if (rr == null) { Debug.LogError("Нет Renderer на redobj!"); return; }

            rr.material = greenmaterial;
            masterkey.provoda1 = true;
        }
        else
        {
            if (gameObject2 == null) { Debug.LogError("gameObject2 НЕ назначен!"); return; }
            if (redobj2 == null) { Debug.LogError("redobj2 НЕ назначен!"); return; }

            Audiomanager.playsound(7);

            var r2 = gameObject2.GetComponent<Renderer>();
            if (r2 == null) { Debug.LogError("Нет Renderer на gameObject2!"); return; }

            r2.material = slomaniuprovod;

            var rr2 = redobj2.GetComponent<Renderer>();
            if (rr2 == null) { Debug.LogError("Нет Renderer на redobj2!"); return; }

            rr2.material = greenmaterial;
            masterkey.provoda2 = true;
        }
    }
}
