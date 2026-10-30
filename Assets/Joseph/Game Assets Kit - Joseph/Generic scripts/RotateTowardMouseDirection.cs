using UnityEngine;
using UnityEngine.UI;

public class RotateTowardMouseDirection : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 10f;
    public bool restrictToYAxis = true;

    [Header("Debug")]
    [SerializeField] private bool showDebugDot = true;

    private Camera mainCamera;
    private Vector3 targetDirection;
    private GameObject debugDot;

    void Start()
    {
        mainCamera = Camera.main;

        if (showDebugDot)
            CreateDebugDot();
    }

    void Update()
    {
        // Rotation logic
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector2 mouseOffset = (Vector2)Input.mousePosition - screenCenter;

        if (Input.mousePosition.x < 0 || Input.mousePosition.y < 0 ||
            Input.mousePosition.x > Screen.width || Input.mousePosition.y > Screen.height)
            return;

        Vector3 flatDirection = new Vector3(mouseOffset.x, 0f, mouseOffset.y).normalized;

        if (flatDirection.sqrMagnitude < 0.001f)
            return;

        targetDirection = restrictToYAxis ? new Vector3(flatDirection.x, 0f, flatDirection.z) : flatDirection;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void CreateDebugDot()
    {
        // Create Canvas
        GameObject canvasGO = new GameObject("DebugCanvas");
        canvasGO.layer = LayerMask.NameToLayer("UI");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.pixelPerfect = true;
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        // Create red dot image
        debugDot = new GameObject("DebugDot");
        debugDot.transform.SetParent(canvasGO.transform);

        Image dotImage = debugDot.AddComponent<Image>();
        dotImage.color = Color.red;

        RectTransform rect = debugDot.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = Vector2.zero;
        rect.sizeDelta = new Vector2(8, 8); // size in pixels
    }

    void OnDestroy()
    {
        if (debugDot != null)
            Destroy(debugDot.transform.parent.gameObject); // destroy canvas and dot
    }
}
