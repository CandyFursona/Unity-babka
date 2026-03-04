using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class masterkey : MonoBehaviour, IUsable
{
    public audiomanager Audiomanager;
    public Game Game;
    public bool provoda1 = false;
    public bool provoda2 = false;
    public bool derevo = false;
    public bool code = false;
    public bool padla_key = false;
    public GameObject text;
    public GameObject gamecanvas;
    public GameObject escapecanvas;
    public GameObject text2;
    public TextMeshProUGUI textms;
    void Start()
    {
        
    }

    public void Use()
    {
        if (provoda1 && provoda2 && derevo && code && padla_key)
        {
            gamecanvas.SetActive(false);
            escapecanvas.SetActive(true);
            StartCoroutine(escapecanvas2());
        }
        else
        {
            StartCoroutine(CantEscapeMessage());
        }
    }

    IEnumerator escapecanvas2()
    {
        Audiomanager.playsound(3);
        Game.hidegrannyandplayer();
        yield return new WaitForSeconds(2f);
        textms.text = "The budget is gone for the cutscene :c \n\n<color=green>(escaped)";
        text2.SetActive(true);
        yield return new WaitForSeconds(2f);
        Game.exit();
    }
    IEnumerator CantEscapeMessage()
    {
        Debug.Log($"{provoda1}, {provoda2}, {derevo}, {code}, {padla_key}");
        yield return new WaitForSeconds(2f);
        text.SetActive(true);
        yield return new WaitForSeconds(2f);
        text.SetActive(false);
        
    }

}
