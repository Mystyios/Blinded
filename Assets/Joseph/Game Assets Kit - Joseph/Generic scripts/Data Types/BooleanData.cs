using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Create
[CreateAssetMenu(
    
    menuName = "DataTypes/Boolean Data",
    order = 1
)]
public class BooleanData : ScriptableObject
{
    public bool value;
    
    public UnityEvent OnValueChanged, TrueEvent, FalseEvent;
    
    //change value
    public void MakeTrue()
    {
            OnValueChanged?.Invoke();
            TrueEvent?.Invoke();
            value = true; 
    }

    public void MakeFalse()
    {
        OnValueChanged?.Invoke();
        FalseEvent?.Invoke();
        value = false;
    }

    public void SwapValue()
    {
        OnValueChanged?.Invoke();
        if (value)
        {
            value = false;
            FalseEvent?.Invoke();
        }
        else
        {
            value = true;
            TrueEvent?.Invoke();
        }
    }
    
}