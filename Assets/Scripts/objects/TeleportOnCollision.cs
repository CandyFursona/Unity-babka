
using System.Collections;
using UnityEngine;

public class TeleportOnCollision : MonoBehaviour
{
    public Transform teleportPoint;
    public GameObject Message;
    IEnumerator ViewMessage()
    {
        Debug.Log("12");
        Message.SetActive(true);
        yield return new WaitForSeconds(3f);
        Message.SetActive(false);
        Debug.Log("11");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (teleportPoint != null)
        {
            Debug.Log("Teleported");
            collision.transform.position = teleportPoint.position;
            StartCoroutine(ViewMessage());
        }
    }
}
