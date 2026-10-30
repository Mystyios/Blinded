using UnityEngine;
using UnityEngine.Events;

public class MoveBehavior : MonoBehaviour
{
    /// <summary>
    /// Single use script for basic movements on object.
    /// Default behavior:
    /// </summary>
    [Header("Start Options")]
    public bool moveOnStart = true;
    public bool moveAfterDelay = false;
    public float delaySeconds = 1f;
    [Header("Initial Velocity Settings")]
    public Vector3 initialVelocity = new Vector3(0f, 0f, 0f);
    public bool useLocalSpace = false;

    [Header("Dynamic Velocity")]
    public bool useAcceleration = false;
    public float accelerationRate = 1f;  // Units per second squared
    public float decelerationRate = 1f;  // Units per second squared
    public Vector3 maxVelocity = new Vector3(100f, 100f, 100f);

    [Header("Negative and 0 settings")]
    public bool boomerang = true; // Allow velocity to reverse past zero
    public UnityEvent onStopEvent;
    //public float dampingrange = 0f; //Range in which the speed will "Damp" or drag to 0. Ex. If ball is 
    
    

    [Header("Runtime")]
    public bool isMoving = false;
    public Vector3 currentVelocity = Vector3.zero; // Exposed for debugging

    private void Start()
    {
        currentVelocity = initialVelocity;

        if (moveOnStart)
        {
            if (moveAfterDelay)
                Invoke(nameof(StartMoving), delaySeconds);
            else
                StartMoving();
        }
    }

    private void Update()
    {
        if (!isMoving) return;

        Vector3 targetDirection = initialVelocity.normalized;

        for (int i = 0; i < 3; i++)
        {
            float axisVelocity = currentVelocity[i];
            float axisInitial = initialVelocity[i];

            if (useAcceleration)
            {
                if (Mathf.Abs(axisInitial) > 0.01f)
                {
                    // Accelerate
                    axisVelocity += Mathf.Sign(axisInitial) * accelerationRate * Time.deltaTime;
                }
                else
                {
                    // Decelerate toward 0
                    float delta = decelerationRate * Time.deltaTime;
                    if (Mathf.Abs(axisVelocity) <= delta)
                        axisVelocity = 0;
                    else
                        axisVelocity -= Mathf.Sign(axisVelocity) * delta;

                    if (!boomerang && axisVelocity == 0)
                        onStopEvent?.Invoke();
                }

                // Clamp to max velocity
                axisVelocity = Mathf.Clamp(axisVelocity, -maxVelocity[i], maxVelocity[i]);
            }
            else
            {
                axisVelocity = axisInitial;
            }

            currentVelocity[i] = axisVelocity;
        }

        Vector3 moveStep = currentVelocity * Time.deltaTime;
        if (useLocalSpace)
            transform.Translate(moveStep, Space.Self);
        else
            transform.Translate(moveStep, Space.World);
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
        currentVelocity = Vector3.zero;
    }
}
