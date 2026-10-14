using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class UseGuiolitine : MonoBehaviour
{
    public audiomanager audiomnager;
    public GameObject lever;
    public GameObject textE;
    public GameObject plrcam;
    private bool canuse = false;
    public Guilotine gl;
    public Transform lesvie;
    public Transform voidforspawn;
    

    public GameObject padlakey;
    public GameObject masterkey;
    public GameObject Safekey;
    private bool preparing = false;
    private int number;

    public void SetInt(int val)
    {
        number = val;
    }

    void destroymelon()
    {
        if (gl == null || gl.MelonModel == null)
            return;

        if (gl.meloninguo)
        {
            Vector3 position = voidforspawn.position;

            GameObject melon = gl.MelonModel;
            gl.MelonModel = null;

            Destroy(melon);

            if (number == 1)
            {
                padlakey.transform.position = position;
                Debug.Log("падла кей");
            }
            else if (number == 2)
            {
                masterkey.transform.position = position;
                Debug.Log("мастер кей");
            }
            else
            {
                Safekey.transform.position = position;
                Debug.Log("Сейф ключ");
            }

            gl.meloninguo = false;
            Debug.Log(number);
        }
    }


    public IEnumerator MoveY()
    {
        preparing = true;
        Vector3 startPos = lesvie.position;
        Vector3 targetPos = new Vector3(
            startPos.x,
            9.30f,
            startPos.z
        );

        float duration = 0.3f; 
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            lesvie.position = Vector3.Lerp(
                startPos,
                targetPos,
                elapsed / duration
            );

            yield return null;
        }

        lesvie.position = targetPos;
        audiomnager.playsound(11);
        destroymelon();
        yield return new WaitForSeconds(3f);

        startPos = lesvie.position;
        targetPos = new Vector3(startPos.x, 10.66f, startPos.z);

        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            lesvie.position = Vector3.Lerp(
                startPos,
                targetPos,
                elapsed / duration
            );

            yield return null;
        }

        lesvie.position = targetPos;
        preparing = false;
    }

    void Update()
    {
        Ray ray = new Ray(
            plrcam.transform.position,
            plrcam.transform.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, 5f))
        {
            //Debug.Log("Попадание: " + hit.collider.name);

            if (hit.collider.gameObject == lever)
            {
                canuse = true;
                textE.SetActive(true);
            }
            else
            {
                canuse = false;
                textE.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && canuse && !preparing)
        {
            Debug.Log("гильонтина испоьзовалась");
            StartCoroutine(MoveY());
        }
    }
}