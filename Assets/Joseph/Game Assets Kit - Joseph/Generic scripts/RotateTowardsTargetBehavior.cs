using UnityEngine;

public class RotateTowardsTargetBehavior : MonoBehaviour
{
    public GameObject target;
    public float RotateSpeed = 90f;
    public bool DebugMode = false;

    public enum TargetVectorAxis { X, Y, Z }
    public TargetVectorAxis targetVector = TargetVectorAxis.X;
    public bool invertVector = false;

    public bool rotateX = false;
    public bool rotateY = true;
    public bool rotateZ = false;

    public bool enableClamping = false;
    public bool clampX = false;
    public float maxXAngle = 45f;
    public bool clampY = false;
    public float maxYAngle = 90f;
    public bool clampZ = false;
    public float maxZAngle = 45f;

    public bool useDamping = false;
    public float dampingFactor = 5f;

    public bool useVariance = false;
    public bool varianceEvolve = false;
    public float varianceSpeed = 1f;
    public float varianceStrength = 20f;
    public float varianceMin = 0f;
    public float varianceMax = 30f;
    public bool varianceReverse = false;

    private Quaternion initialLocalRotation;

    public bool targetPlayeronStart = false;
    void Start()
    {
        if (target == null)
            if (targetPlayeronStart){target = GameObject.FindGameObjectWithTag("Player");}
            

        initialLocalRotation = transform.localRotation;
    }

    public void Retarget(GameObject newtarget)
    {
        target = newtarget;
    }
    void Update()
    {
        if (target == null) return;

        Vector3 toTarget = (target.transform.position - transform.position).normalized;
        Vector3 localAimingAxis = GetLocalTargetVector();

        Vector3 variance = Vector3.zero;
        if (useVariance)
        {
            float varianceCurrent = varianceStrength;
            if (varianceEvolve)
            {
                varianceCurrent = Mathf.PingPong(Time.time * varianceSpeed, varianceMax - varianceMin) + varianceMin;
            }
            else
            {
                if (varianceCurrent > varianceMax) varianceCurrent = varianceMax;
            }

            float osc = Mathf.Sin(Time.time * varianceSpeed * Mathf.PI * 2);
            if (varianceReverse) osc = -osc;

            float amount = varianceCurrent * osc;
            variance = new Vector3(
                rotateX ? amount : 0f,
                rotateY ? amount : 0f,
                rotateZ ? amount : 0f
            );
        }

        RotateOnAxis(Vector3.right, localAimingAxis, toTarget, rotateX, clampX, maxXAngle, variance.x);
        RotateOnAxis(Vector3.up, localAimingAxis, toTarget, rotateY, clampY, maxYAngle, variance.y);
        RotateOnAxis(Vector3.forward, localAimingAxis, toTarget, rotateZ, clampZ, maxZAngle, variance.z);
    }

    private void RotateOnAxis(Vector3 localAxis, Vector3 localAimingAxis, Vector3 toTarget, bool rotate, bool clamp, float maxAngle, float varianceAmount)
    {
        if (!rotate) return;

        Vector3 worldAxis = transform.TransformDirection(localAxis);
        Vector3 aimingDirection = transform.TransformDirection(localAimingAxis);

        Vector3 projectedTarget = Vector3.ProjectOnPlane(toTarget, worldAxis).normalized;
        Vector3 projectedCurrent = Vector3.ProjectOnPlane(aimingDirection, worldAxis).normalized;

        float angle = Vector3.SignedAngle(projectedCurrent, projectedTarget, worldAxis);
        float step = useDamping ? angle * Mathf.Clamp01(dampingFactor * Time.deltaTime)
                                 : Mathf.Clamp(angle, -RotateSpeed * Time.deltaTime, RotateSpeed * Time.deltaTime);

        step += varianceAmount;

        if (enableClamping && clamp)
        {
            Vector3 delta = GetLocalAxisAngleDelta();
            float current = Vector3.Dot(localAxis, delta);
            if (Mathf.Abs(current + step) > maxAngle)
            {
                if (DebugMode) Debug.Log("Rotation clamped on " + localAxis);
                return;
            }
        }

        transform.Rotate(worldAxis, step, Space.World);

        if (DebugMode)
        {
            Debug.Log($"Axis: {localAxis}, Angle: {angle}, Step: {step}");
        }
    }

    private Vector3 GetLocalTargetVector()
    {
        switch (targetVector)
        {
            case TargetVectorAxis.Y: return invertVector ? -Vector3.up : Vector3.up;
            case TargetVectorAxis.Z: return invertVector ? -Vector3.forward : Vector3.forward;
            default: return invertVector ? -Vector3.right : Vector3.right;
        }
    }

    private Vector3 GetLocalAxisAngleDelta()
    {
        Quaternion delta = Quaternion.Inverse(initialLocalRotation) * transform.localRotation;
        Vector3 euler = delta.eulerAngles;
        euler.x = Mathf.DeltaAngle(0f, euler.x);
        euler.y = Mathf.DeltaAngle(0f, euler.y);
        euler.z = Mathf.DeltaAngle(0f, euler.z);
        return euler;
    }
}
