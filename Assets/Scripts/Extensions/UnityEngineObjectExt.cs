using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class UnityEngineObjectExt
{
    public static List<T> FindObjectsOfInterface<T>()
    {
        List<T> results = new List<T>();

        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var root in rootObjects)
        {
            results.AddRange(root.GetComponentsInChildren<T>(true));
        }

        return results;
    }
}
