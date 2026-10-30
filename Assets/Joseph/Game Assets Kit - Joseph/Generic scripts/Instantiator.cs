using UnityEngine;

public class Instantiator : MonoBehaviour
{
    public GameObject prefab; // Assign in the Inspector

    public bool WorldRotation = false;
    
    // Call this method to instantiate the prefab
    public void MakeInstance()
    {
        if (prefab != null)
        {
            if (WorldRotation == false)
            {
                Instantiate(prefab, transform.position, transform.rotation);

            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity);

            }
        }
        else
        {
            Debug.LogWarning("Prefab is not assigned.");
        }
    }
    
}