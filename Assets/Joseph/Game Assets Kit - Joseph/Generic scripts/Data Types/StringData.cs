using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(
    
    menuName = "DataTypes/String Data"
)]public class StringData : ScriptableObject
{
    public string value;
    
    public UnityEvent OnValueChanged;

    public void updateString(string newValue)
    {
        value = newValue;
    }
    public void concatenateString(string newValue)
    {
        value += newValue;
    }
    public bool searchString(string findString)
    {
        if (findString.Contains(value))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
}
