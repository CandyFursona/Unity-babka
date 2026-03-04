using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    public killing killingclass;
    public bool gamete = false;
    public PickupPlayer PickUpPlayerClass;
    public Pause pauseclass;
    public int day;
    public GameObject player;
    public GameObject granny;
    public GameObject camera1;
    public GameObject camera2;
    public GameObject canvas; // menu
    public GameObject canvas2; // game
    public GameObject grannyprefab;
    public GameObject canvas3; // day-interval
    public GameObject canvas4; // escape-failed
    public TextMeshProUGUI text2;
    public TextMeshProUGUI daytext;
    public GameObject thelastdaywarning;
    public masterkey masterkeysett;
    public GameObject pausecanvas;

    [Header("preset")]
    public GameObject padla_key;
    public GameObject dopderevyashka; // к padla_key
    public GameObject provoda1; // provoda1
    public GameObject provoda2; // provoda2
    public GameObject derevo; // derevo
    public GameObject code; // padlock_code

    public GameObject red1; // provoda1
    public GameObject red2; // provoda2
    public Material redmaterial;
    public Material normalprovoda;

    [Header("Items")]
    public GameObject Padlakeyitem; // синий ключ
    public GameObject Hammeritem; // молоток
    public GameObject padlockcodeitem; // кодовый замок
    public GameObject MasterKeyitem; // красный ключ
    public GameObject nippersitem; // кусачки
    public GameObject wasa; // ваза




    public void startgame()
    {
        StartCoroutine(StartGameFirst());
    }
    IEnumerator StartGameFirst() // НЕ ТРОГАТЬ Я НЕ ЕБУ КАК ОНО РАБОТАЕТ
    {
        gamete = true;
        PickUpPlayerClass.removeiteminhand(PickUpPlayerClass.heldItem);
        text2.text = "";
        thelastdaywarning.SetActive(false);
        daytext.text = "";

        masterkeysett.provoda1 = false; 
        masterkeysett.provoda2 = false;
        masterkeysett.derevo = false;
        masterkeysett.code = false;
        masterkeysett.padla_key = false;

        wasa.transform.position = new Vector3(1.247564f, 15.27608f, -2.250522f);
        wasa.transform.rotation = Quaternion.Euler(0f, 41.524f, 0f);

        Padlakeyitem.transform.position = new Vector3(15.1473f, 10.0665f, 19.463f);
        Padlakeyitem.transform.rotation = Quaternion.Euler(0, 0, 0);

        Hammeritem.transform.position = new Vector3(9.644f, 15.192f, -3.221f);
        Hammeritem.transform.rotation = Quaternion.Euler(0f, 0f, -96.022f);

        padlockcodeitem.transform.position = new Vector3(8.97f, 10.457f, -5.855f);
        padlockcodeitem.transform.rotation = Quaternion.Euler(0, 0, 0);

        MasterKeyitem.transform.position = new Vector3(-4.511f, 14.86886f, 7.348f);
        MasterKeyitem.transform.rotation = Quaternion.Euler(0, 0, 0);

        nippersitem.transform.position = new Vector3(1.503f, 9.8f, 17.358f);
        nippersitem.transform.rotation = Quaternion.Euler(0f, 0f, -91.979f);

        code.GetComponent<Rigidbody>().isKinematic = true;
        code.transform.position = new Vector3(15.787f, 12.874f, 3.72f);
        code.transform.rotation = Quaternion.Euler(90, 0, 90);

        padla_key.GetComponent<Rigidbody>().isKinematic = true;
        padla_key.transform.position = new Vector3(15.787f, 12.037f, 3.73f);
        padla_key.transform.rotation = Quaternion.Euler(90, 0, 90);

        dopderevyashka.GetComponent<Rigidbody>().isKinematic = true;
        dopderevyashka.transform.position = new Vector3(15.792f, 12.053f, 5.322f);
        dopderevyashka.transform.rotation = Quaternion.Euler(90, 0, 0);

        provoda1.GetComponent<Renderer>().material = normalprovoda;
        provoda2.GetComponent<Renderer>().material = normalprovoda;

        derevo.GetComponent<Rigidbody>().isKinematic = true;
        derevo.transform.position = new Vector3(15.792f, 9.623f, 5.322f);
        derevo.transform.rotation = Quaternion.Euler(90, 0, 0);

        red1.GetComponent<Renderer>().material = redmaterial;
        red2.GetComponent<Renderer>().material = redmaterial;

        day = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        canvas.SetActive(false);
        canvas3.SetActive(true);
        yield return new WaitForSeconds(3f);
        daytext.text = $"day {day}";
        yield return new WaitForSeconds(1f);
        canvas3.SetActive(false);

        player.SetActive(true);
        camera1.SetActive(false);
        canvas2.SetActive(true);
        canvas.SetActive(false);
        camera2.SetActive(true);
        granny.SetActive(true);
        grannyprefab.SetActive(false);
        var cc = player.GetComponent<CharacterController>();
        cc.enabled = false;
        player.transform.SetPositionAndRotation(new Vector3(-3.69f, 14.957f, -7.63f), Quaternion.Euler(0, 0, 0));
        cc.enabled = true;

        var agent = granny.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.Warp(new Vector3(4.906f, 5.968f, 5.588f));
        if (player.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.constraints = RigidbodyConstraints.None;
        }

        if (granny.TryGetComponent<Rigidbody>(out Rigidbody rb2))
        {
            rb2.constraints = RigidbodyConstraints.None;
        }
        granny.GetComponent<NavAgent>().ForcePatrol();
        granny.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
        Destroy(rb);
        player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        Debug.Log(1);
    }

    public void newday()
    {
        StartCoroutine(Cutscene());
    }

    IEnumerator Cutscene()
    {
        gamete = true;
        text2.text = "";
        Debug.Log("try to restart day");
        day += 1;
        if (day == 5)
        {
            thelastdaywarning.SetActive(true);
        }
        if (day >= 6)
        {
            StartCoroutine(Failed());
            yield break;
        }
        canvas.SetActive(false);
        canvas3.SetActive(true);
        daytext.text = $"day {day}";
        Debug.Log(1);
        yield return new WaitForSeconds(3f);
        Debug.Log(0);
        canvas3.SetActive(false);
        killingclass.kill = false;
        if (player.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.constraints = RigidbodyConstraints.None;
        }

        if (granny.TryGetComponent<Rigidbody>(out Rigidbody rb2))
        {
            rb2.constraints = RigidbodyConstraints.None;
        }
        
        player.transform.position = new Vector3(-3.69f, 14.957f, -7.63f);
        granny.transform.position = new Vector3(4.906f, 5.968f, 5.588f);
        granny.GetComponent<NavAgent>().ForcePatrol();
        granny.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
        Destroy(rb);
        player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        Debug.Log(1);
    }

    IEnumerator Failed()
    {
        gamete = false;
        PickUpPlayerClass.removeiteminhand(PickUpPlayerClass.heldItem);
        text2.text = "";
        canvas4.SetActive(true);
        yield return new WaitForSeconds(2f);
        text2.text = "The budget is gone for the cutscene :c \n\n<color=red>(failed)";
        yield return new WaitForSeconds(3f);
        exit();
    }
    public void hidegrannyandplayer()
    {
        player.SetActive(false);
        camera1.SetActive(true);
        granny.SetActive(false);
    }
    public void resetgame()
    {
        day = 0;
        granny.SetActive(false);
        player.SetActive(false);
        if (player.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.constraints = RigidbodyConstraints.None;
        }

        if (granny.TryGetComponent<Rigidbody>(out Rigidbody rb2))
        {
            rb2.constraints = RigidbodyConstraints.None;
        }
        granny.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
    }

    public void exit()
    {
        gamete = false;
        PickUpPlayerClass.removeiteminhand(PickUpPlayerClass.heldItem);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.SetActive(false);
        camera1.SetActive(true);
        canvas2.SetActive(false);
        canvas.SetActive(true);
        canvas3.SetActive(false);
        canvas4.SetActive(false);
        camera2.SetActive(false);
        pauseclass.pause = false;
        granny.SetActive(false);
        grannyprefab.SetActive(true);
    }

}
