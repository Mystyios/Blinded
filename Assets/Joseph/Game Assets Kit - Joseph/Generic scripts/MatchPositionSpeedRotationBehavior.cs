using System;
using UnityEngine;

public class MatchPositionSpeedRotationBehavior : MonoBehaviour
{
    [Header("Gameobject to match")]

    public GameObject Target;

    public bool FindByName;
    public string targetName;
    public bool FindByTag;
    public string targetTag;

    [Header("Behavior")]
    public bool matchPosition = true;
    public bool matchRotation = false;
    [Tooltip("Matches Velocity of object by matching its position with its current offset.")]
    public bool matchV3Velocity = false;

    public bool MatchActive = true;
    private Vector3 previousTargetPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (FindByName)
        {
            Target = GameObject.Find(targetName);
        }

        if (FindByTag)
        {
            Target = GameObject.FindWithTag(targetTag);
        }

        if (Target)
        {
            previousTargetPosition = Target.transform.position;

        }
        
    }

    public void SetTarget(GameObject obj)
    {
        Target = obj;
        previousTargetPosition = Target.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
        matchPositionToTarget();
        matchRotationToTarget();
        //matchV3VelocityToTarget();
        
    }

    private void LateUpdate()
    {
        matchV3VelocityToTarget();
    }

    private void matchPositionToTarget()
    {
        if (!matchPosition)
        {return;}
        this.transform.position = Target.transform.position;
        
    }

    private void matchRotationToTarget()
    {
        if (!matchRotation)
        { return; }
        this.transform.rotation = Target.transform.rotation;
    }

    private void matchV3VelocityToTarget()
    {
        if (!matchV3Velocity)
        { return; }
        
        Vector3 changeInPosDelta = Target.transform.position - previousTargetPosition;
        
        this.transform.position += changeInPosDelta;//Match with an offset
        previousTargetPosition = Target.transform.position;
    }
    private void startMatchingPosition()
    {
        if (!matchPosition)
        {
            matchPosition = true;
        }
    }

    private void startMatchingRotation()
    {
        if (!matchRotation)
            {
                matchRotation = true;
            }
    }

    private void startMatchingV3Velocity()
    {
        if (!matchV3Velocity)
            {
                matchV3Velocity = true;
            }
    }

    private void stopMatchingPosition()
    {
        if (!matchPosition)
        {
            matchPosition = false;
        }
    }

    private void stopMatchingRotation()
    {
        if (!matchRotation)
            {
                matchRotation = false;
            }
    }

    private void stopMatchingV3Velocity()
    {
        if (!matchV3Velocity)
        {
            matchV3Velocity = false;
        }
    }
}
