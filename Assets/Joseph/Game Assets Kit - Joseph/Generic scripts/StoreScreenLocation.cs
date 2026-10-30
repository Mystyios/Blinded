using UnityEngine;
using UnityEngine.InputSystem;

public class StoreScreenLocation : MonoBehaviour
{
    public Vector2 Screen_Location;

    public bool Store_in_Vector2_data_type = false;
    public Vector2Data Screen_Location_data;
    public bool Debug_Mode = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Screen_Location.x = screenPoint.x;
        Screen_Location.y = screenPoint.y;
        
        if (Store_in_Vector2_data_type)
        {
            Screen_Location_data.UpdateValueV2(Screen_Location);
        }
        
        
    }
}
