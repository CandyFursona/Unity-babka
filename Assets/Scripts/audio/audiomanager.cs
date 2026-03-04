using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class audiomanager : MonoBehaviour
{
    public AudioClip itemdrop;
    public AudioClip granny;
    public AudioClip escape;
    public AudioClip hammer;
    public AudioClip bat;
    public AudioClip zamok;
    public AudioClip electric;
    public AudioClip music;
    public AudioSource audioSource;

    public Slider volume2;

    public void playsound(int val)
    {
        switch (val)
        {
            case 1:
                audioSource.PlayOneShot(itemdrop); // fall.cs
                break;

            case 2:
                audioSource.PlayOneShot(granny); // NavAgnt.cs
                break;

            case 3:
                audioSource.PlayOneShot(escape); // masterkey.cs
                break;

            case 4:
                audioSource.PlayOneShot(hammer); // Derevyshka.cs (1)
                break;

            case 5:
                audioSource.PlayOneShot(bat); // killing.cs
                break;

            case 6:
                audioSource.PlayOneShot(zamok); // Derevyshka.cs (2), padlakey.cs
                break;

            case 7:
                audioSource.PlayOneShot(electric); // Nippers.cs
                break;

            default:
                Debug.Log(val);
                break;
        }
    }

    private void Update()
    {
        audioSource.volume = volume2.value * 3;
    }
}
