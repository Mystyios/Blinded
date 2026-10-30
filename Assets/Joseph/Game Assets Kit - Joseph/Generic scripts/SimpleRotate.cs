using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    [Header("Enable Rotation Axes")]
    public bool rotateX = false;
    public bool rotateY = true;
    public bool rotateZ = false;

    [Header("Rotation Speeds (degrees per second)")]
    public float speedX = 90f;
    public float speedY = 90f;
    public float speedZ = 90f;

    void Update()
    {
        float x = rotateX ? speedX * Time.deltaTime : 0f;
        float y = rotateY ? speedY * Time.deltaTime : 0f;
        float z = rotateZ ? speedZ * Time.deltaTime : 0f;

        transform.Rotate(x, y, z);
    }
}