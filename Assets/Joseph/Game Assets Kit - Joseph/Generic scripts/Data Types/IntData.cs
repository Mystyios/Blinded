using System;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(
    
    menuName = "DataTypes/Int Data",
    order = 3
)]
public class IntData : ScriptableObject
{
    public int value;

    public int defaultValue;

    public UnityEvent onChangeEvent, onDecreaseEvent, onIncreaseEvent, onResetEvent;
    public void SetValue(int num)
    {
        value = num;
        onChangeEvent?.Invoke();
    }

    

    public void SetValue(IntData obj)
    {
        value = obj.value;
    }

    public void SubtractValue(IntData obj)
    {
        if(value>0)
            value -= obj.value;
    }

    public void AddValue(IntData obj)
    {
        value += obj.value;
    }
    
    public void UpdateValue(int num)
    {
        int x = value;
        value += num;
        onChangeEvent?.Invoke();
        if (value > x)
        {
            onIncreaseEvent?.Invoke();
        }
        else
        {
            onDecreaseEvent?.Invoke();
        }
        
    }

    public void ResetToDefault()
    {
        value = defaultValue;
        onResetEvent?.Invoke();
    }

    public void SetDefault(int defaultValue)
    {
        this.defaultValue = defaultValue;
    }
    
    public void AddDefaultValue()
    {
        int x = value;
        value += defaultValue;
        onChangeEvent?.Invoke();
        if (value > x)
        {
            onIncreaseEvent?.Invoke();
        }
        else
        {
            onDecreaseEvent?.Invoke();
        }
    }
}