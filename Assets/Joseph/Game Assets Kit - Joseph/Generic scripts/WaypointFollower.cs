using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class WaypointFollower : MonoBehaviour
{
    [Header("Path Settings")]
    public Vector3DataList path;

    [Tooltip("Automatically starts following the path in Start().")]
    public bool moveOnStart = true;

    [Tooltip("Rotate to face the next waypoint while moving.")]
    public bool faceNextWaypoint = false;

    [Tooltip("After reaching the end, walk backwards along the same path.")]
    public bool reversePath = false;

    [Tooltip("Teleport back to the first waypoint and loop forever when finished.")]
    public bool teleportToStartOnStop = false;

    [Header("Speed Settings")]
    [Tooltip("Use IntData asset to control speed.")]
    public bool useIntDataSpeed = false;

    public IntData speedData;
    public float moveSpeed = 3f;

    [Header("Start Delay")]
    [Tooltip("Wait this many seconds before beginning movement.")]
    public float startDelay = 0f;

    [Header("Events")]
    public UnityEvent onDelayStarted;
    public UnityEvent onDelayFinished;
    public UnityEvent onPathFinished;
    public UnityEvent onMovementStopped;

    private int currentIndex = 0;
    private int direction = 1;
    private bool isMoving = false;
    private bool isDelaying = false;
    private float delayTimer = 0f;

    void Start()
    {
        if (!ValidatePath()) { enabled = false; return; }

        transform.position = path.Vector3Dlist[0].value;

        if (moveOnStart)
        {
            StartMovement();
        }
    }

    void Update()
    {
        if (isDelaying)
        {
            delayTimer -= Time.deltaTime;
            if (delayTimer <= 0f)
            {
                isDelaying = false;
                isMoving = true;
                onDelayFinished?.Invoke();
            }
            return;
        }

        if (!isMoving || !ValidatePath()) return;

        float speed = useIntDataSpeed && speedData != null ? speedData.value : moveSpeed;
        Vector3 target = path.Vector3Dlist[currentIndex].value;

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (faceNextWaypoint)
        {
            Vector3 dir = (target - transform.position).normalized;
            if (dir.sqrMagnitude > 0.0001f)
                transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * 10f);
        }

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            AdvanceIndex();
        }
    }

    public void StartMovement()
    {
        if (!ValidatePath()) return;

        StopAllCoroutines();

        transform.position = path.Vector3Dlist[0].value;
        direction = 1;
        currentIndex = 1;

        if (startDelay > 0f)
        {
            delayTimer = startDelay;
            isDelaying = true;
            isMoving = false;
            onDelayStarted?.Invoke();
        }
        else
        {
            isDelaying = false;
            isMoving = true;
        }
    }

    public void StopMovement()
    {
        isMoving = false;
        isDelaying = false;
        StopAllCoroutines();
        onMovementStopped?.Invoke();

        if (teleportToStartOnStop && ValidatePath())
        {
            transform.position = path.Vector3Dlist[0].value;
        }
    }

    private void AdvanceIndex()
    {
        currentIndex += direction;

        bool reachedEndForward = direction == 1 && currentIndex >= path.Vector3Dlist.Count;
        bool reachedEndReverse = direction == -1 && currentIndex < 0;

        if (reachedEndForward || reachedEndReverse)
        {
            if (teleportToStartOnStop)
            {
                transform.position = path.Vector3Dlist[0].value;
                direction = 1;
                currentIndex = 1;
                onPathFinished?.Invoke();
                return;
            }

            if (reversePath && path.Vector3Dlist.Count > 1)
            {
                direction *= -1;
                currentIndex += direction == 1 ? 1 : -1;
                return;
            }

            isMoving = false;
            onPathFinished?.Invoke();

            if (teleportToStartOnStop)
                transform.position = path.Vector3Dlist[0].value;
        }
    }

    private bool ValidatePath()
    {
        return path != null && path.Vector3Dlist != null && path.Vector3Dlist.Count > 0;
    }
}
