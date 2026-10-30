using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private Rigidbody rb;
    private bool isKnockedBack = false;
    private float knockbackDuration = 0.5f;
    private float knockbackTimer = 0f;

    [SerializeField] private float knockbackForce = 10f;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.Sleep();
    }

    void Update()
    {
        if (isKnockedBack)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0f || rb.linearVelocity.magnitude < 0.1f)
            {
                EndKnockback();
            }
        }
    }

    public void ApplyKnockback(Vector3 hitDirection)
    {
        if (!isKnockedBack)
        {
            isKnockedBack = true;
            knockbackTimer = knockbackDuration;

            navAgent.enabled = false;
            rb.isKinematic = false;
            rb.WakeUp();
            rb.AddForce(hitDirection.normalized * knockbackForce, ForceMode.Impulse);
        }
    }

    private void EndKnockback()
    {
        isKnockedBack = false;
        rb.isKinematic = true;
        rb.Sleep();
        if (!navAgent.enabled)
        {
            navAgent.enabled = true;
            navAgent.ResetPath();
        }
    }
}