
using UnityEngine;

[CreateAssetMenu(
    
    menuName = "DataTypes/Vector3 Data",
    order = 4
)]

public class Vector3Data : ScriptableObject
{
    public Vector3 value;
    
    public void UpdateValueVector3Data(Vector3Data newData)
    {
        value = newData.value;
    }

    public void UpdateValueTransform(Transform transformobj)
    {
        value = transformobj.position;
    }
    public void UpdateValueV3(Vector3 obj)
    {
        value = obj;
    }

    public void addValueV3(Vector3 obj)
    {
        value += obj;
    }
    public void moveObjecttoValue(GameObject obj){
        obj.transform.position = value;
        Debug.Log(obj.name + " moved to " + obj.transform.position);
    }

    public void moveCharactertoPoint(GameObject obj)
    {   
        //Special method required for character controllers as they override obj position. 
        obj.GetComponent<CharacterController>().enabled = false;
        obj.transform.position = value;
        obj.GetComponent<CharacterController>().enabled = true;
    }
    
}
