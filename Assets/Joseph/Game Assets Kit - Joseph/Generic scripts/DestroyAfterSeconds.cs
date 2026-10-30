using UnityEngine;
using UnityEngine.Events;

public class DestroyAfterSeconds : MonoBehaviour
{
    [Tooltip("How many seconds to wait before destroying this object.")]
    public float lifetime = 5f;

    [Tooltip("Called right before the object is destroyed.")]
    public UnityEvent beforeDestroy;

    void Start()
    {
        // Start the timed destruction
        StartCoroutine(DestroyAfterDelay());
    }

    private System.Collections.IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(lifetime);
        TriggerDestroy();
    }

    public void DestroyNow()
    {
        TriggerDestroy();
    }

    private void TriggerDestroy()
    {
        // Invoke any events
        beforeDestroy?.Invoke();

        // Destroy the object
        Destroy(gameObject);
    }
}