using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Reflection;

public class ScriptableObjectFinder : MonoBehaviour
{
    public static T GetInstantiate<T>(string objectName) where T : ScriptableObject
    {
        if (objectName == null || objectName == "null")
            return null;

        string subfolder = typeof(T).ToString() + "s";
        string fullPath = Path.Combine(subfolder, objectName);

        T loadedObject = Resources.Load<T>(fullPath);
        if (loadedObject != null)
        {
            string loadedObjName = loadedObject.name;
            loadedObject = Instantiate<T>(loadedObject);
            loadedObject.name = loadedObjName;

            return loadedObject;
        }
        else
        {
            return null;
        }
    }
    public static T Get<T>(string objectName) where T : ScriptableObject
    {
        return Get(objectName, typeof(T)) as T;
    }
    public static UnityEngine.Object Get(string objectName, Type type)
    {
        if (objectName == null || objectName == "null" || string.IsNullOrEmpty(objectName))
            return null;

        UnityEngine.Object returnedObject = null;
        string subfolder = type.ToString() + "s";
        while(returnedObject == null && type.BaseType != typeof(ScriptableObject))
        {
            type = type.BaseType;
            subfolder = Path.Combine(type.ToString() + "s", subfolder);
        }

        string fullPath = Path.Combine(subfolder, objectName);

        returnedObject = Resources.Load(fullPath, type);

        if (returnedObject == null)
        {
            subfolder = type.ToString() + "s";
            returnedObject = GetObjectByPath(subfolder, objectName, type);
        }

        if (returnedObject == null)
        {
            subfolder = "";
            returnedObject = GetObjectByPath(subfolder, objectName, type);//Test is
        }

        return returnedObject;
    }

    private static UnityEngine.Object GetObjectByPath(string path, string objectName, Type type)
    {
        UnityEngine.Object[] objectsOfType = Resources.LoadAll(path, type);

        for (int i = 0; i < objectsOfType.Length; i++)
        {
            if (objectsOfType[i].name == objectName)
            {
                return objectsOfType[i];
            }
        }

        return null;
    }
}