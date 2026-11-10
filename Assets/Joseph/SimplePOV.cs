using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimplePOV : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeed = 2f;
    public bool doGravity = false;
    public float gravity = -9.8f;
    public GameObject cameraObject;

    private CharacterController characterController;
    
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        float z = Input.GetAxisRaw("Vertical"); // W/S or Up/Down
        //move character based on the cameras vector3.forward
        
        Vector3 move = cameraObject.transform.right * x + cameraObject.transform.forward * z;
        
        // apply gravity
        move.y = 0;
        if (doGravity)
        {
            move = new Vector3(move.x* moveSpeed * Time.deltaTime, move.y + gravity * Time.deltaTime, move.z* moveSpeed * Time.deltaTime);
            characterController.Move(move);
        }
        else
        {
            characterController.Move(move * moveSpeed * Time.deltaTime);
        }


        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;
        cameraObject.transform.Rotate(-mouseY, mouseX, 0);
        //Lock camera rotation z so it is always 0
        cameraObject.transform.rotation = Quaternion.Euler(cameraObject.transform.eulerAngles.x, cameraObject.transform.eulerAngles.y, 0);  

    }
    


}
