using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(
    
    menuName = "DataTypes/Gameobject List"
)]

public class GameObjectDataList : ScriptableObject
{
    [Tooltip("All tracked GameObjects")]
    public List<GameObject> GameObjects = new List<GameObject>();
    

    // ------------------------------
    // Retrieval Functions
    // ------------------------------

    public void clearList()
    {
        GameObjects.Clear();
    }
    public GameObject GetGameObject(int id)
    {
        if (id < 0 || id >= GameObjects.Count) return null;
        return GameObjects[id];
    }

    public GameObject GetRandom()
    {
        if (GameObjects.Count == 0) return null;
        return GameObjects[Random.Range(0, GameObjects.Count)];
    }

    public List<GameObject> GetAllObjects()
    {
        return new List<GameObject>(GameObjects);
    }

    public List<GameObject> GetObjectsRangeFront(int count)
    {
        count = Mathf.Clamp(count, 0, GameObjects.Count);
        return GameObjects.GetRange(0, count);
    }

    public List<GameObject> GetObjectsRangeBack(int count)
    {
        count = Mathf.Clamp(count, 0, GameObjects.Count);
        return GameObjects.GetRange(GameObjects.Count - count, count);
    }

    // ------------------------------
    // Destruction Helpers
    // ------------------------------

    private void TryDestroy(GameObject obj)
    {
        if (obj == null) return;

        var trigger = obj.GetComponent<DestroyWithTriggerBehavior>();
        if (trigger != null)
        {
            trigger.DestroywithTrigger();
        }
        else
        {
            Object.Destroy(obj);
        }
    }

    public void TriggerDeathByID(int id)
    {
        var obj = GetGameObject(id);
        if (obj != null)
        {
            TryDestroy(obj);
            GameObjects.RemoveAt(id);
        }
    }

    public void DestroyRandom()
    {
        if (GameObjects.Count == 0) return;
        int id = Random.Range(0, GameObjects.Count);
        TriggerDeathByID(id);
    }

    public void DestroyAll()
    {
        // Clean out null / missing references first
        GameObjects.RemoveAll(obj => obj == null);

        for (int i = GameObjects.Count - 1; i >= 0; i--)
        {
            TriggerDeathByID(i);
        }
    }


    public void DestroyRangeFront(int count)
    {
        count = Mathf.Clamp(count, 0, GameObjects.Count);
        for (int i = count - 1; i >= 0; i--)
        {
            TriggerDeathByID(i);
        }
    }

    public void DestroyRangeBack(int count)
    {
        count = Mathf.Clamp(count, 0, GameObjects.Count);
        for (int i = 0; i < count; i++)
        {
            TriggerDeathByID(GameObjects.Count - 1);
        }
    }

    // ------------------------------
    // Extra Utilities
    // ------------------------------

    public void AddObject(GameObject obj)
    {
        GameObjects.Add(obj);
        
    }

    public void RemoveObject(GameObject obj)
    {
        if (GameObjects.Contains(obj))
            GameObjects.Remove(obj);
    }
    

    public GameObject FindByName(string name)
    {
        return GameObjects.Find(o => o != null && o.name == name);
    }

    public int CountValid()
    {
        GameObjects.RemoveAll(o => o == null);
        return GameObjects.Count;
    }
    private void OnEnable()
    {
        GameObjects.Clear(); // always start fresh in Play mode
    }

}
