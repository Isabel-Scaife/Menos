using UnityEditor;
using UnityEngine;

public class MissingScriptFinder
{
    [MenuItem("Tools/Find Missing Scripts In Scene")]
    private static void FindMissingScripts()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        int count = 0;
        foreach (GameObject go in allObjects)
        {
            Component[] components = go.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    Debug.Log($"Missing script on GameObject: '{go.name}' in scene '{go.scene.name}'", go);
                    count++;
                }
            }
        }

        Debug.Log($"Finished scan. Found {count} GameObjects with missing scripts.");
    }
}
