using UnityEngine;
using UnityEngine.InputSystem;
public class RotateInput : MonoBehaviour
{
    public FloatData rotatespeed;

    

    private Vector2 targetDirection;

    public InputActionReference Target_Direction;
    public bool rotate_on_y_axis = true;
    public bool rotate_on_z_axis = false;
    public bool rotate_on_x_axis = false;
    public bool negate_small_deadzone = true;
    public bool DebugMode = false;
    public float deadzone = 0.5f;

    public float rotate_offset = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetDirection = Target_Direction.action.ReadValue<Vector2>();
        
        if (targetDirection == Vector2.zero)
        {
            return;
        }

        if (targetDirection.magnitude <= (Vector2.one * deadzone).magnitude)
        {
            return;
        }
        rotatetowards(targetDirection);
        if (!DebugMode)
        {
            return;
        }
        Debug.Log("Object " + gameObject.name + " is rotating towards " + targetDirection );
    }

    void rotatetowards(Vector2 direction)
    {
        
        float targetangle = Mathf.Atan2(targetDirection.x, targetDirection.y) * Mathf.Rad2Deg;
        targetangle += rotate_offset;
        //Lerp towards target angle
        if (rotate_on_y_axis)
        {
            gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, targetangle, gameObject.transform.eulerAngles.z);
        }
        

    }
}
