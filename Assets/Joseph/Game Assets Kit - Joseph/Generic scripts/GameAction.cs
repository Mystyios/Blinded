using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu]
public class GameAction : ScriptableObject
{
    public UnityAction raise;
    public bool DebugMode = false;
    public string DebugString = "GameActionName";
    public void RaiseAction(){
        raise?.Invoke();
        if(!DebugMode){return;}
        Debug.Log(DebugString + "- GameAction Raised with Debug");
       }
    
}
