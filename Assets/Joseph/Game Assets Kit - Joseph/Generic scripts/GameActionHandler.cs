using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameActionHandler : MonoBehaviour
{
    public GameAction gameActionObj;
    public UnityEvent onRaiseEvent;
    public bool doDebug = false;
    private void Start()
    {
        gameActionObj.raise += Raise;
    }

    private void Raise()
    {
        onRaiseEvent.Invoke();
        if (!doDebug)
        {
            return;
        }
        Debug.Log("Received Action to raise from " + gameActionObj.name);
    }
}
