using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class SimpleBoolData : ScriptableObject
{
    public bool value;
    
    public UnityEvent OnValueChanged, TrueEvent, FalseEvent;
    
    //change value
    public void MakeTrue()
    {
        OnValueChanged.Invoke();
        TrueEvent.Invoke();
        value = true;
    }

    public void MakeFalse()
    {
        OnValueChanged.Invoke();
        FalseEvent.Invoke();
        value = false;
    }
}
