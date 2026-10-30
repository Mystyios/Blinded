using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SimpleCoroutineBehavior : MonoBehaviour
{
    // Public variable to set the delay in seconds
    public float seconds = 1f;

    // Event to be triggered after the delay
    public UnityEvent OnDelayComplete;

    // Method to start the coroutine
    public void StartDelay()
    {
        StartCoroutine(DelayCoroutine());
    }

    // Coroutine that waits for the specified seconds and then invokes the event
    private IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(seconds);
        OnDelayComplete?.Invoke(); // Trigger the event if it's not null
    }
}