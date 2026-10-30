using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class WaypointListPopulator : MonoBehaviour
{
    [Header("Set to true to generate a new Vector3DataList asset")]
    public bool createNewList = false;

    [Tooltip("If 'Create New List' is false, populate this with your Vector3DataList asset.")]
    public Vector3DataList targetList;

    [ContextMenu("Populate From Children")]
    public void PopulateFromChildren()
    {
#if !UNITY_EDITOR
        Debug.LogError("This script requires the UnityEditor namespace to create assets.");
        return;
#endif

        string parentName = gameObject.name;

        // Create new list if requested
        if (createNewList || targetList == null)
        {
            targetList = ScriptableObject.CreateInstance<Vector3DataList>();
            targetList.Vector3Dlist = new List<Vector3Data>();

            // Save new asset to Assets folder
            string assetPath = $"Assets/{parentName}_List.asset";
            assetPath = AssetDatabase.GenerateUniqueAssetPath(assetPath);
            AssetDatabase.CreateAsset(targetList, assetPath);

            Debug.Log($"Created new Vector3DataList asset at: {assetPath}", this);
        }

        // Ensure the list is cleared and ready
        if (targetList.Vector3Dlist == null)
            targetList.Vector3Dlist = new List<Vector3Data>();
        else
            targetList.Vector3Dlist.Clear();

        // Populate list from children
        int index = 1;
        foreach (Transform child in transform)
        {
            var data = ScriptableObject.CreateInstance<Vector3Data>();
            data.value = child.position;

            string dataName = $"{parentName}_{index}";
            data.name = dataName;

#if UNITY_EDITOR
            AssetDatabase.AddObjectToAsset(data, targetList);
#endif

            targetList.Vector3Dlist.Add(data);
            index++;
        }

#if UNITY_EDITOR
        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(targetList);
#endif

        Debug.Log($"Populated '{targetList.name}' with {targetList.Vector3Dlist.Count} waypoints from {parentName}.", this);
    }
}
