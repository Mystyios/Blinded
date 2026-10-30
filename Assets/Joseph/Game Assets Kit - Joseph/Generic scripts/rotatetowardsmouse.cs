using UnityEngine;
using System.Collections;
public class rotatetowardsmouseRaycast : MonoBehaviour
{

    //Notes are not mine, in the process of making scripts i have more clear
    public bool DebugDot = false;
    void Update () 
    {
        //Mouse Position in the world. It's important to give it some distance from the camera. 
        //If the screen point is calculated right from the exact position of the camera, then it will
        //just return the exact same position as the camera, which is no good.
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);
		
        //Angle between mouse and this object
        float angle = AngleBetweenPoints(transform.position, mouseWorldPosition);
		angle += 180;
        //Ta daa
        transform.rotation =  Quaternion.Euler (new Vector3(0f,angle,0f));
        if (!DebugDot)
        {
            return;
        }
        Debug.DrawLine(transform.position, mouseWorldPosition, Color.red);
        
    }
	
    float AngleBetweenPoints(Vector2 a, Vector2 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
	
}