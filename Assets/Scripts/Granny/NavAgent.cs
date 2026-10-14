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
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject obj;

    [Header("Movement")]
    [SerializeField] private float patrolSpeed = 1.5f;
    [SerializeField] private float chaseSpeed = 2.5f;
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

    [Header("Vars")]
    [SerializeField] private DoorBoom doorBoom;
    [SerializeField] private StreetDetector streetDetector;

    [Header("Forced Point")]
    [SerializeField] private Transform point14;

    private bool isForcedToPoint14 = false;

    [Header("Sound Investigation")]
    [SerializeField] private float investigateWaitSeconds = 3f;
    [SerializeField] private float investigateReachDistance = 0.8f;
    private bool isInvestigatingSound = false;
    private bool isWaitingAtSound = false;
    private float investigateWaitUntil = 0f;

    private int currentPatrolIndex = 0;
    private bool isChasing = false;
    private float lastTimeSawPlayer = -999f;
    private float nextRepathTime = 0f;
    private Vector3 forcedDestination;
    public bool grannymustspawn = true;

    // Player Speed = 6f
    public void ChangeVars(int val, int ps, int cs)
    {
        if (val == 0) // practice
        {
            patrolSpeed = 0;
            chaseSpeed = 0;
            grannymustspawn = false;
        }
        if (val == 1) // easy
        {
            patrolSpeed = 1.5f;
            chaseSpeed = 2.5f;
            grannymustspawn = true;
        }
        else if (val == 2) // normal
        {
            patrolSpeed = 3.5f;
            chaseSpeed = 4.5f;
            grannymustspawn = true;
        }
        else if (val == 3) // hard
        {
            patrolSpeed = 4.5f;
            chaseSpeed = 5.5f;
            grannymustspawn = true;
        }
        else if (val == 4) // extreme
        {
            patrolSpeed = 6.0f;
            chaseSpeed = 7.5f;
            grannymustspawn = true;
        } else if (val == 5) // extreme+
        {
            patrolSpeed = 7.5f;
            chaseSpeed = 9.0f;
            grannymustspawn = true;
        } else if (val == 6) // extreme++
        {
            patrolSpeed = 9.0f;
            chaseSpeed = 10.5f;
            grannymustspawn = true;
        } else if (val == 7) // impossible
        {
            patrolSpeed = 10.5f;
            chaseSpeed = 12.0f;
            grannymustspawn = true;
        } 
        else if (val == 8) // custom 
        {
            Debug.Log(ps);
            Debug.Log(cs);
            patrolSpeed = ps;
            chaseSpeed = cs;
        }

    }

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

    public void InvestigateSound(Vector3 worldPosition)
    {

        if (isForcedToPoint14 || isChasing)
            return;

        if (agent == null || !agent.enabled || !agent.isOnNavMesh)
            return;

        if (!NavMesh.SamplePosition(
                worldPosition,
                out NavMeshHit hit,
                4f,
                agent.areaMask))
        {
            return;
        }
        bool wasInvestigatingSound = isInvestigatingSound;
        isInvestigatingSound = true;
        isWaitingAtSound = false;

        agent.isStopped = false;
        agent.speed = chaseSpeed;
        agent.acceleration = 8f;
        agent.autoBraking = true;
        agent.stoppingDistance = 0f;

        agent.ResetPath();

        if (!agent.SetDestination(hit.position))
        {
            isInvestigatingSound = false;
        }

        if (!wasInvestigatingSound && Audiomanager != null)
        {
            Audiomanager.playsound(9);
        }
    }

    private void HandleSoundInvestigation()
    {
        if (agent == null || !agent.enabled || !agent.isOnNavMesh)
            return;

        if (isWaitingAtSound)
        {
            if (Time.time >= investigateWaitUntil)
            {
                isWaitingAtSound = false;
                isInvestigatingSound = false;

                EnterPatrol();

                if (patrolPoints != null && patrolPoints.Length > 0)
                    GoToPatrolPoint(currentPatrolIndex);
            }

            return;
        }

        agent.speed = chaseSpeed;

        if (!agent.pathPending &&
            agent.hasPath &&
            agent.remainingDistance <=
            Mathf.Max(agent.stoppingDistance, investigateReachDistance))
        {
            agent.isStopped = true;
            agent.ResetPath();

            agent.speed = patrolSpeed;

            isWaitingAtSound = true;
            investigateWaitUntil = Time.time + investigateWaitSeconds;
        }
    }

    private void Update()
    {

        if (doorBoom != null && !doorBoom.nevibita)
        {
            if (isForcedToPoint14)
            {
                isForcedToPoint14 = false;

                if (agent != null && agent.enabled && agent.isOnNavMesh)
                {
                    agent.ResetPath();
                    agent.isStopped = false;
                }
            }
        }

        else if (doorBoom != null &&
                 doorBoom.nevibita &&
                 streetDetector != null &&
                 streetDetector.isInStreet &&
                 isChasing)
        {
            if (!isForcedToPoint14)
            {
                isForcedToPoint14 = true;

                if (agent != null && agent.enabled && agent.isOnNavMesh)
                {
                    agent.ResetPath();
                    agent.isStopped = false;
                    agent.speed = patrolSpeed;

                    if (point14 != null)
                        agent.SetDestination(point14.position);
                }
            }

            if (agent != null && agent.enabled && agent.isOnNavMesh)
                agent.isStopped = false;

            return;
        }
       
        if (isForcedToPoint14)
            return;

        if (player == null)
            return;

        bool seesPlayer = player != null && CanSeePlayer();

        if (seesPlayer)
        {
            lastTimeSawPlayer = Time.time;

            if (!isChasing)
                EnterChase();
        }
        else if (isChasing &&
                 Time.time - lastTimeSawPlayer >= loseTargetAfterSeconds)
        {
            EnterPatrol();

            if (patrolPoints != null && patrolPoints.Length > 0)
                GoToPatrolPoint(currentPatrolIndex);
        }

        if (isChasing)
        {
            HandleChase();
        }
        else if (isInvestigatingSound)
        {
            HandleSoundInvestigation();
        }
        else
        {
            HandlePatrol();
        }
    }


    private void EnterChase()
    {
        isInvestigatingSound = false;
        isWaitingAtSound = false;

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
        isInvestigatingSound = false;
        isWaitingAtSound = false;

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