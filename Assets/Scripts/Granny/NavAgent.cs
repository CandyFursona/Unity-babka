using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class NavAgent : MonoBehaviour
{
    public audiomanager Audiomanager;
    [Header("References")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private Transform[] patrolPoints;

    [Header("Movement")]
    [SerializeField] private float patrolSpeed = 2.0f;
    [SerializeField] private float chaseSpeed = 4.5f;
    [SerializeField] private float waypointReachDistance = 0.8f;
    [SerializeField] private float repathInterval = 0.15f;

    [Header("Vision")]
    [SerializeField] private float viewDistance = 12f;
    [Range(1f, 180f)]
    [SerializeField] private float viewAngle = 180f;
    [SerializeField] private float eyeHeight = 1f;
    [SerializeField] private LayerMask obstacleMask; // стены
    [SerializeField] private LayerMask targetMask;   // игрок
    [SerializeField] private LayerMask floorMask;    // пол
    [SerializeField] private float loseTargetAfterSeconds = 10f;

    [Header("Debug")]
    [SerializeField] private bool drawDebug = true;

    private int currentPatrolIndex = 0;
    private bool isChasing = false;
    private float lastTimeSawPlayer = -999f;
    private float nextRepathTime = 0f;
    private Vector3 forcedDestination;
    private void Reset()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Awake()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
    }



    private void Start()
    {
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent не найден на объекте врага.");
            enabled = false;
            return;
        }

        if (!agent.isOnNavMesh)
        {
            Debug.LogWarning("Агент не стоит на NavMesh. Проверь Bake/позицию/Agent Type.");
        }

        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            agent.speed = patrolSpeed;
            GoToPatrolPoint(currentPatrolIndex);
        }
    }

    private void Update()
    {
        if (player == null) return;

        bool seesPlayer = CanSeePlayer();

        if (seesPlayer)
        {
            lastTimeSawPlayer = Time.time;
            if (!isChasing) EnterChase();
        }
        else if (isChasing && Time.time - lastTimeSawPlayer >= loseTargetAfterSeconds)
        {
            EnterPatrol();
            if (patrolPoints != null && patrolPoints.Length > 0)
                GoToPatrolPoint(currentPatrolIndex);
        }

        if (isChasing)
        {
            HandleChase();
        }
        else
        {
            HandlePatrol();
        }
    }


    private void EnterChase()
    {
        Audiomanager.playsound(2);
        isChasing = true;
        agent.speed = chaseSpeed;
        agent.acceleration = 20f;          
        agent.autoBraking = false;         
        agent.stoppingDistance = 0.1f;     
        agent.isStopped = false;
        agent.ResetPath();
        nextRepathTime = 0f;
    }

    private void EnterPatrol()
    {

        isChasing = false;
        agent.speed = patrolSpeed;
        agent.acceleration = 8f;
        agent.autoBraking = true;
        agent.stoppingDistance = 0f;
        agent.isStopped = false;
        agent.ResetPath();
    }

    private void HandlePatrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;

        if (agent == null || !agent.enabled || !agent.isOnNavMesh) return;
        agent.speed = patrolSpeed;

        if (!agent.pathPending && agent.remainingDistance <= Mathf.Max(agent.stoppingDistance, waypointReachDistance))
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            GoToPatrolPoint(currentPatrolIndex);
        }
    }

    private void HandleChase()
    {
        if (agent == null || !agent.enabled || !agent.isOnNavMesh) return;
        if (Time.time >= nextRepathTime)
        {
            nextRepathTime = Time.time + repathInterval;

            if (NavMesh.SamplePosition(player.position, out var hit, 2f, agent.areaMask))
                agent.SetDestination(hit.position);
            else
                agent.SetDestination(player.position);
        }
    }

    private void GoToPatrolPoint(int index)
    {
        if (agent == null || !agent.enabled || !agent.isOnNavMesh) return;
        if (patrolPoints == null || patrolPoints.Length == 0) return;
        if (index < 0 || index >= patrolPoints.Length) return;

        agent.SetDestination(patrolPoints[index].position);
    }

    private bool CanSeePlayer()
    {
        Vector3 eyePos = transform.position + Vector3.up * eyeHeight;
        Vector3 playerPos = player.position + Vector3.up * eyeHeight;
        Vector3 toPlayer = playerPos - eyePos;

        float dist = toPlayer.magnitude;
        if (dist > viewDistance) return false;

        Vector3 flat = Vector3.ProjectOnPlane(toPlayer, Vector3.up);
        if (flat.sqrMagnitude > 0.0001f)
        {
            float angle = Vector3.Angle(transform.forward, flat.normalized);
            if (angle > viewAngle * 0.5f) return false;
        }

        int combinedMask = obstacleMask | floorMask | targetMask;

        if (Physics.Raycast(eyePos, toPlayer.normalized, out RaycastHit hit, dist, combinedMask, QueryTriggerInteraction.Ignore))
        {
            int hitLayerMask = 1 << hit.collider.gameObject.layer;
            bool isTarget = (targetMask.value & hitLayerMask) != 0;


            //Debug.Log($"Ray hit: {hit.collider.name}, layer: {LayerMask.LayerToName(hit.collider.gameObject.layer)}");

            return isTarget;
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        if (!drawDebug) return;

        Vector3 eyePos = transform.position + Vector3.up * eyeHeight;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(eyePos, viewDistance);

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(eyePos, transform.forward * viewDistance);

        if (player != null)
        {

            Gizmos.color = CanSeePlayer() ? Color.green : Color.red;
            Gizmos.DrawLine(eyePos, player.position + Vector3.up * eyeHeight);
        }
    }

    public void ForcePatrol()
    {
        EnterPatrol();

        if (patrolPoints != null && patrolPoints.Length > 0)
            GoToPatrolPoint(currentPatrolIndex);
    }

    private void HandleGoToPoint()
    {
        if (agent == null || !agent.enabled || !agent.isOnNavMesh) return;


        if (Time.time >= nextRepathTime)
        {
            nextRepathTime = Time.time + repathInterval;

            if (NavMesh.SamplePosition(forcedDestination, out var hit, 2f, agent.areaMask))
                agent.SetDestination(hit.position);
            else
                agent.SetDestination(forcedDestination);
        }

        if (!agent.pathPending && agent.remainingDistance <= Mathf.Max(agent.stoppingDistance, waypointReachDistance))
        {
            EnterPatrol();
            if (patrolPoints != null && patrolPoints.Length > 0)
                GoToPatrolPoint(currentPatrolIndex);
        }
    }

    public void RunTo(Vector3 worldPos)
    {
        EnterChase();


        forcedDestination = worldPos;

        if (agent != null && agent.enabled && agent.isOnNavMesh)
        {
            if (NavMesh.SamplePosition(worldPos, out var hit, 2f, agent.areaMask))
                agent.SetDestination(hit.position);
            else
                agent.SetDestination(worldPos);
        }
    }
}