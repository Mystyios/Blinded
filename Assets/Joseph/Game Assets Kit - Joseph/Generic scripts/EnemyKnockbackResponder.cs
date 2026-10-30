using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class EnemyKnockbackResponder : MonoBehaviour
{
    public float knockbackCooldown = 0.2f;
    private Rigidbody rb;
    private bool canBeKnocked = true;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ApplyKnockback(Vector3 direction, float force)
    {
        if (!canBeKnocked) return;
        

        rb.AddForce(direction * force, ForceMode.Impulse);
        StartCoroutine(KnockbackCooldown());
    }

    private System.Collections.IEnumerator KnockbackCooldown()
    {
        canBeKnocked = false;
        yield return new WaitForSeconds(knockbackCooldown);
        canBeKnocked = true;
    }
}