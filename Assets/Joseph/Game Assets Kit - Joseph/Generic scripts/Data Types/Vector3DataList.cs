using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(
    
    menuName = "DataTypes/Vector3 Data List"
    
)]

public class Vector3DataList : ScriptableObject
{
    public List<Vector3Data> Vector3Dlist = new List<Vector3Data>();

    public Vector3Data GetVector3Data(int id)
    {
        return Vector3Dlist[id];
    }
    public Vector3Data GetRandom()
    {
        return Vector3Dlist[Random.Range(0,Vector3Dlist.Count)];
    }

    
    
}
