using UnityEngine;

public class StoreObjectVelocity : MonoBehaviour
{
    public bool store_Vector3Data = true;
    public Vector3Data Vector3Data;
    public bool store_magnitude_as_floatData = true;
    public FloatData FloatData;

    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (store_Vector3Data)
        {
            Vector3Data.value = rb.linearVelocity;
        }

        if (store_magnitude_as_floatData)
        {
            FloatData.value = rb.linearVelocity.magnitude;
        }
        
    }
}
