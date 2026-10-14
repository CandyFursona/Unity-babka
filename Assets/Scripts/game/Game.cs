using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Game : MonoBehaviour
{
    public NavAgent grn;
    public GameObject grny;
    public GameConfig cfg;
    public UseGuiolitine guiolitine;
    public Transform[] SpawnPoints;

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
    public GameObject Which;
    public GameObject Melon;
    public GameObject SafeKey;

    [Header("Items")]
    public TMP_Text SeedUI;
    public GameObject SdUI;





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

        if (cfg.Objpreset == 1)
        {
            guiolitine.SetInt(1); // padlakey
            //Padlakeyitem.transform.position = SpawnPoints[0].position;
            Hammeritem.transform.position = SpawnPoints[11].position;
            padlockcodeitem.transform.position = SpawnPoints[1].position;
            MasterKeyitem.transform.position = SpawnPoints[8].position;
            nippersitem.transform.position = SpawnPoints[4].position;
            Which.transform.position = SpawnPoints[5].position;
            Melon.transform.position = SpawnPoints[7].position;
            SafeKey.transform.position = SpawnPoints[12].position;
        }
        if (cfg.Objpreset == 2)
        {
            guiolitine.SetInt(3); // safekey
            Padlakeyitem.transform.position = SpawnPoints[0].position;
            Hammeritem.transform.position = SpawnPoints[4].position;
            padlockcodeitem.transform.position = SpawnPoints[12].position;
            nippersitem.transform.position = SpawnPoints[7].position;
            Melon.GetComponent<Rigidbody>().isKinematic = true;
            Melon.transform.position = SpawnPoints[13].position;
            Melon.transform.SetParent(SpawnPoints[13]);
            Which.transform.position = SpawnPoints[8].position;
            MasterKeyitem.transform.position = SpawnPoints[6].position;
        }
        if (cfg.Objpreset == 3)
        {
            guiolitine.SetInt(2); // master key
            Padlakeyitem.transform.position = SpawnPoints[2].position;
            Hammeritem.GetComponent<Rigidbody>().isKinematic = true;
            Hammeritem.transform.position = SpawnPoints[13].position;
            Hammeritem.transform.SetParent(SpawnPoints[13]);

            padlockcodeitem.transform.position = SpawnPoints[8].position;
            nippersitem.transform.position = SpawnPoints[5].position;
            Melon.transform.position = SpawnPoints[7].position;
            Which.transform.position = SpawnPoints[9].position;
            SafeKey.transform.position = SpawnPoints[3].position;
        }
        if (cfg.Objpreset == 4)
        {
            guiolitine.SetInt(1);
            padlockcodeitem.GetComponent<Rigidbody>().isKinematic = true;
            padlockcodeitem.transform.position = SpawnPoints[13].position;
            padlockcodeitem.transform.SetParent(SpawnPoints[13]);
            Hammeritem.transform.position = SpawnPoints[5].position;
            nippersitem.transform.position = SpawnPoints[8].position;
            Melon.transform.position = SpawnPoints[9].position;
            Which.transform.position = SpawnPoints[3].position;
            SafeKey.transform.position = SpawnPoints[6].position;
            MasterKeyitem.transform.position = SpawnPoints[10].position;

        }
        if (cfg.Objpreset == 5)
        {
            guiolitine.SetInt(3);
            Melon.GetComponent<Rigidbody>().isKinematic = true;
            Melon.transform.position = SpawnPoints[13].position;
            Melon.transform.SetParent(SpawnPoints[13]);
            Hammeritem.transform.position = SpawnPoints[9].position;
            nippersitem.transform.position = SpawnPoints[5].position;
            padlockcodeitem.transform.position = SpawnPoints[8].position;
            Which.transform.position = SpawnPoints[0].position;
            MasterKeyitem.transform.position = SpawnPoints[3].position;
            Padlakeyitem.transform.position = SpawnPoints[2].position;
        }
        if (cfg.Objpreset == 6)
        {
            int? seedInput = cfg.ObjSeed;

            int seed = (!seedInput.HasValue || seedInput.Value == 0)
                ? UnityEngine.Random.Range(100000, 1000000)
                : seedInput.Value;

            SdUI.SetActive(true);
            SeedUI.text = $"Seed: {seed.ToString()}";

            System.Random rng = new System.Random(seed);

            Debug.Log("Random preset seed: " + seed);

            int target = rng.Next(1, 4); 
            guiolitine.SetInt(target);

            GameObject excludedItem = null;

            if (target == 1)
                excludedItem = Padlakeyitem;
            else if (target == 2)
                excludedItem = MasterKeyitem;
            else if (target == 3)
                excludedItem = SafeKey;

            GameObject[] allItems =
            {
                Padlakeyitem,
                Hammeritem,
                padlockcodeitem,
                MasterKeyitem,
                nippersitem,
                Which,
                Melon,
                SafeKey
            };

            List<GameObject> objects = new List<GameObject>();

            foreach (GameObject item in allItems)
            {
                if (item != null && item != excludedItem)
                    objects.Add(item);
            }

            List<int> positions = new List<int>();

            bool validLayout = false;

            while (!validLayout)
            {
                for (int i = objects.Count - 1; i > 0; i--)
                {
                    int j = rng.Next(i + 1);

                    GameObject temp = objects[i];
                    objects[i] = objects[j];
                    objects[j] = temp;
                }

                positions.Clear();

                for (int i = 0; i < 14; i++)
                    positions.Add(i);

                for (int i = positions.Count - 1; i > 0; i--)
                {
                    int j = rng.Next(i + 1);

                    int temp = positions[i];
                    positions[i] = positions[j];
                    positions[j] = temp;
                }

                int safeKeyPos = -1;
                int whichPos = -1;
                int melonPos = -1;

                for (int i = 0; i < objects.Count; i++)
                {
                    if (objects[i] == SafeKey)
                        safeKeyPos = positions[i];

                    if (objects[i] == Which)
                        whichPos = positions[i];

                    if (objects[i] == Melon)
                        melonPos = positions[i];
                }

                validLayout = true;

                if (safeKeyPos == 7)
                    validLayout = false;

                if (safeKeyPos == 13 && whichPos == 7)
                    validLayout = false;

                if (target == 3 && melonPos == 7)
                    validLayout = false;
            }

            for (int i = 0; i < objects.Count; i++)
            {
                GameObject item = objects[i];
                int pos = positions[i];

                if (item == excludedItem)
                    continue;

                item.transform.position = SpawnPoints[pos].position;

                if (pos == 13)
                {
                    Rigidbody rb3 = item.GetComponent<Rigidbody>();

                    if (rb3 != null)
                        rb3.isKinematic = true;
                    
                    item.transform.position = SpawnPoints[13].position;
                    item.transform.SetParent(SpawnPoints[13]);
                }
            }

            Debug.Log(
                "Random preset generated. Seed: " + seed +
                ", SetInt: " + target
            );
        }

        grny.GetComponent<NavMeshAgent>().autoBraking = cfg.UseDrift;

        /*
         List<Transform> points = new List<Transform>(SpawnPoints);

            for (int i = 0; i < points.Count; i++)
            {
                int randomIndex = Random.Range(i, points.Count);

                Transform temp = points[i];
                points[i] = points[randomIndex];
                points[randomIndex] = temp;
            }

            Transform Point1 = points[0];
            Transform Point2 = points[1];
            Transform Point3 = points[2];
            Transform Point4 = points[3];
            Transform Point5 = points[4];
            Transform Point6 = points[5];
            Transform Point7 = points[6];
            Transform Point8 = points[7];

        */

        /*
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
        */
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
        if (grn.grannymustspawn)
        {
            granny.SetActive(true);
        }
        grannyprefab.SetActive(false);
        var cc = player.GetComponent<CharacterController>();
        cc.enabled = false;
        player.transform.SetPositionAndRotation(new Vector3(-3.69f, 14.957f, -7.63f), Quaternion.Euler(0, 0, 0));
        cc.enabled = true;
        var agent = granny.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.Warp(new Vector3(4.906f, 5.968f, 5.588f));
        if (grn.grannymustspawn)
        {


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
            Destroy(rb);
        }   
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
        player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        Debug.Log(1);
        yield return new WaitForSeconds(1f);
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
        if (grn.grannymustspawn)
        {
            granny.GetComponent<NavAgent>().ForcePatrol();
            granny.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        }
        NavMeshAgent agent = granny.GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            bool wasEnabled = agent.enabled;

            agent.enabled = false;
            granny.transform.position = new Vector3(4.906f, 5.968f, 5.588f);
            agent.enabled = wasEnabled;

            if (agent.enabled)
            {
                agent.Warp(new Vector3(4.906f, 5.968f, 5.588f));
                agent.ResetPath();
            }
        }
        player.transform.position = new Vector3(-3.69f, 14.957f, -7.63f);
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
        Destroy(rb);
        player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        Debug.Log(1);
        
    }

    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject SnowScreen;
    [SerializeField] private VideoClip failedvideo;

    IEnumerator Failed()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gamete = false;
        PickUpPlayerClass.removeiteminhand(PickUpPlayerClass.heldItem);
        text2.text = "";
        canvas4.SetActive(true);
        SnowScreen.SetActive(true);
        videoPlayer.clip = failedvideo;
        yield return new WaitForSeconds(2f);

        videoPlayer.Play();

        yield return new WaitForSeconds(12f);

        ReloadScene();
    }
    public void hidegrannyandplayer()
    {
        player.SetActive(false);
        camera1.SetActive(true);
        granny.SetActive(false);
    }

    public void resetgame()
    {
        ReloadScene();
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f; // на случай, если игра была на паузе

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void exit()
    {
        ReloadScene();
    }

}
