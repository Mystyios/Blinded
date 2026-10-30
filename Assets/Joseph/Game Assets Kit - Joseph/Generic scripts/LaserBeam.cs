using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserBeam : MonoBehaviour
{
    [Header("Laser Settings")]
    public float length = 10f;
    public float width = 0.2f;
    public Color laserColor = Color.red;
    [Range(0, 10)]
    public float brightness = 5f;
    public bool laserOn = true;

    private LineRenderer lr;
    private Material laserMaterial;
    public bool IsDebug = false;
    //public GameObject DebugIndicator;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = false;
        lr.positionCount = 2;

        // Use HDR color with emission
        laserMaterial = new Material(Shader.Find("Unlit/Color"));
        lr.material = laserMaterial;

        UpdateLaser();
    }

    void Update()
    {
        if (laserOn)
        {
            if (!lr.enabled)
                lr.enabled = true;

            UpdateLaser();
        }
        else
        {
            if (lr.enabled)
                lr.enabled = false;
        }
    }

    void UpdateLaser()
    {
        // Set width
        lr.startWidth = width;
        lr.endWidth = width;

        // Set laser direction from origin to X-forward
        lr.SetPosition(0, Vector3.zero);
        lr.SetPosition(1, Vector3.right * length);

        // Brightness via HDR color
        Color hdrColor = laserColor * brightness;
        laserMaterial.color = hdrColor;
    }

    public void SetLaserActive(bool state)
    {
        laserOn = state;
    }
}