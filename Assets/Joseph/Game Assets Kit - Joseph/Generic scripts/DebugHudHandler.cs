using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class DebugHudHandler : MonoBehaviour
{

    public TMP_Text DebugMainText;
    public InputActionReference DebugInput;
    public UnityEvent DebugModeOnEvent, DebugModeOffEvent, DebugModeHoldEvent;

    public List<GameObject> DebugObjectsToggle = new List<GameObject>();
    public List<GameObject> TempDebugObjects = new List<GameObject>();
    public List<GameObject> SpawnDebugObjects = new List<GameObject>();

    public bool SpawnDebugObjectsOnStart = false;
    private bool debugModeOn = false;

    public Transform Player;
    private float deltaTime = 0.0f;

    public StringData relevantText;
    
    public FloatData PlayerVelocity;
    public Vector3Data PlayerVelocityVector;
    public GameObjectDataList enemiesDataList;
    private void OnEnable()
    {
        DebugInput.action.canceled += DebugmodeOnOff;
    }

    private void OnDisable()
    {
        DebugInput.action.canceled -= DebugmodeOnOff;
    }

    private void DebugmodeOnOff(InputAction.CallbackContext ctx)
    {
        if (debugModeOn)
        {
            DebugModeOff();
            debugModeOn = false;
        }
        else
        {
            DebugModeOn();
            debugModeOn = true;
        }
    }

    public void DebugModeOn()
    {
        DebugModeOnEvent?.Invoke();
        TempDebugObjects.Clear();
        foreach (GameObject obj in DebugObjectsToggle)
        {
            obj.SetActive(true);
        }
    }

    public void DebugModeOff()
    {
        DebugModeOffEvent?.Invoke();
        TempDebugObjects.Clear();
        foreach (GameObject obj in DebugObjectsToggle)
        {
            obj.SetActive(false);
            if (obj.tag == "TempDebugDelete")
            {
                Destroy(obj);
            }
        }
    }

    public void SpawnDebugObjectsFromList()
    {
        UnityEngine.Debug.Log("DEBUGGING!");
        foreach (GameObject obj in SpawnDebugObjects)
        {
            Instantiate(obj, transform.position, transform.rotation);
        }
    }

    private void Update()
    {
        if (debugModeOn)
        {
            DebugTextUpdate();

        }
    }

    public void DebugTextUpdate()
    {
        string playerPosstring = Player != null ? $"Player Pos: {Player.position.x:F2}, {Player.position.y:F2}, {Player.position.z:F2}"
            : "Player: N/A";
        
        
        float msec = deltaTime * 1000.0f;
        long totalMemory = System.GC.GetTotalMemory(false) / (1024 * 1024); // MB
        float fps = 1.0f / deltaTime;
        string fpsstring = 
            $"FPS: {fps:F1} ({msec:F1} ms)\n" +
            $"Memory: {totalMemory} MB\n" +
            $"Time: {System.DateTime.Now:HH:mm:ss}" ;
        string paddedvelocityvector =
            $"{PlayerVelocityVector.value.ToString("+0.00;-0.00; +0.00").PadLeft(7)} ";

        string Enemies = "Current enemies : " + enemiesDataList.GameObjects.Count;
        DebugMainText.text = fpsstring + "\n" + playerPosstring + "\n" + $"Player Velocity : {PlayerVelocity.value}\n Player Velocity Vector : {paddedvelocityvector} \n" + relevantText.value + $"\n{Enemies} \n" ;

    }
}
