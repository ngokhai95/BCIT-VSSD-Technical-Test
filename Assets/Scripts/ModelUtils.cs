using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ModelUtils
{
    // Get all descendants of a game object
    public static List<GameObject> GetAllDescendants(GameObject gameObject)
    {
        List<GameObject> descendants = new List<GameObject>();
        GetDescendants(gameObject.transform, descendants);
        return descendants;
    }

    // Helper function to recursively get all descendants
    private static void GetDescendants(Transform transform, List<GameObject> descendants)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            descendants.Add(child.gameObject);
            GetDescendants(child, descendants);
        }
    }
}
