using System.Collections;
using UnityEngine;

public class killing : MonoBehaviour
{
    public audiomanager Audiomanager;
    [Header("���� ����")]
    public Game gamesett;
    public GameObject player;
    public GameObject cam;
    public Transform target;                
    public float triggerDistance = 2f;

    [Header("�����")]
    public LayerMask stenuMask;              
    public LayerMask floorMask;             

    [Header("�������� ����")]
    public Transform groundCheckPoint;      // nll
    public float groundCheckRadius = 0.2f;
    public bool killingnow = false;
    private bool wasTriggered;

    public bool kill;

    void Update()
    {
        if (target == null) return;

        float dist = Vector3.Distance(transform.position, target.position);
        if (dist > triggerDistance)
        {
            wasTriggered = false; 
            return;
        }

        //if (!IsOnFloor()) return;

        if (HasWallBetween()) return;

        if (!wasTriggered)
        {
            wasTriggered = true;
            OnNearTarget();
        }
    }

    bool IsOnFloor()
    {
        Vector3 p = groundCheckPoint ? groundCheckPoint.position : transform.position;
        return Physics.CheckSphere(p, groundCheckRadius, floorMask);
    }

    bool HasWallBetween()
    {
        Vector3 from = transform.position + Vector3.up * 1.0f;
        Vector3 to = target.position + Vector3.up * 1.0f;

        return Physics.Linecast(from, to, stenuMask);
    }

    void OnNearTarget()
    {
        killingnow = true;
        StartCoroutine(KillPlayer());
    }

    IEnumerator KillPlayer()
    {
        if (kill == false)
        {
            kill = true;
            player.GetComponent<PickupPlayer>().Drop(player.GetComponent<PickupPlayer>().heldItem);
            player.AddComponent<Rigidbody>();
            gameObject.transform.LookAt(player.transform);
            Rigidbody rb2 = gameObject.GetComponent<Rigidbody>();
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            rb2.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.LookAt(gameObject.transform);
            player.GetComponent<PlayerController>().enabled = false;
            Rigidbody rb = player.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            yield return new WaitForSeconds(1f);
            Quaternion target = Quaternion.Euler(0f, -10f, 0f);
            Audiomanager.playsound(5);
            while (Quaternion.Angle(transform.rotation, target) > 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target, 1000f * Time.deltaTime);
                yield return null;
            }
            Quaternion target2 = Quaternion.Euler(0f, 100f, 0f);
            while (Quaternion.Angle(transform.rotation, target2) > 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target2, 1000f * Time.deltaTime);
                yield return null; 
            }
            
            rb.constraints = RigidbodyConstraints.None;
            Vector3 direction = (player.transform.position - transform.position).normalized;
            player.GetComponent<CapsuleCollider>().enabled = true;
            rb.AddForce(direction * 10f, ForceMode.Impulse);

            yield return new WaitForSeconds(2f);
            gamesett.newday();
            killingnow = false;
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
    }
}