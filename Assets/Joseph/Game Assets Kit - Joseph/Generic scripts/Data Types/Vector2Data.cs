
using UnityEngine;

[CreateAssetMenu(
    
    menuName = "DataTypes/Vector2 Data"
    
)]

public class Vector2Data : ScriptableObject
{
    public Vector2 value;
    
    public void UpdateValueVector2Data(Vector2Data newData)
    {
        value = newData.value;
    }

    public void UpdateValueTransform(Transform transformobj)
    {
        value.x = transformobj.position.x;
        value.y = transformobj.position.z;
    }
    public void UpdateValueV2(Vector2 obj)
    {
        value = obj;
    }

    public void addValueV2(Vector2 obj)
    {
        value += obj;
    }
    

    
    
}
