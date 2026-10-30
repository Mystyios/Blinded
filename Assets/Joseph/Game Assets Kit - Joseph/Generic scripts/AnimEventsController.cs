using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEventsController : MonoBehaviour
{
    [System.Serializable]
    public class TimedAnimationEvent
    {
        [Range(0f, 1f)]
        public float normalizedTime;
        public UnityEvent onEvent;
        [HideInInspector] public bool triggered = false;
    }

    [System.Serializable]
    public class AnimationEventTrigger
    {
        public string animationStateName; // Exact name of the animation state
        public List<TimedAnimationEvent> events = new List<TimedAnimationEvent>();
    }

    [Header("Animator Reference")]
    public Animator animator;

    [Header("Animation Triggers")]
    public List<AnimationEventTrigger> animationTriggers = new List<AnimationEventTrigger>();

    void Update()
    {
        if (animator == null || animationTriggers.Count == 0)
            return;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        string currentState = stateInfo.IsName("") ? "" : stateInfo.shortNameHash.ToString();
        float normalizedTime = stateInfo.normalizedTime % 1f;

        foreach (var trigger in animationTriggers)
        {
            if (stateInfo.IsName(trigger.animationStateName))
            {
                foreach (var animEvent in trigger.events)
                {
                    if (!animEvent.triggered && normalizedTime >= animEvent.normalizedTime)
                    {
                        animEvent.triggered = true;
                        animEvent.onEvent?.Invoke();
                    }
                }
            }
            else
            {
                // Reset events when not in animation
                foreach (var animEvent in trigger.events)
                {
                    animEvent.triggered = false;
                }
            }
        }
    }
}