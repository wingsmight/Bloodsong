using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    public static string saveFolder = Application.persistentDataPath
#if UNITY_EDITOR
        + "_EDITOR"
#endif
        ;

    private const string EXTENSION = ".wmg";


    public static void SaveStoredDatas<T>(List<T> storedDatas) where T : IStoredData
    {
        SaveStoredDatas<T>(storedDatas, CurGameSlot);
    }
    public static void SaveStoredDatas<T>(List<T> storedDatas, int slotIndex) where T : IStoredData
    {
        foreach (var storedDataToSave in storedDatas)
        {
            if (storedDataToSave != null)
            {
                SaveStoredData<T>(storedDataToSave, slotIndex);
            }
        }
    }
    public static void SaveStoredData<T>(T storedData) where T : IStoredData
    {
        SaveStoredData<T>(storedData, CurGameSlot);
    }
    public static void SaveStoredData<T>(T storedData, int slotIndex) where T : IStoredData
    {
        StoredDataSerializer formatter = new StoredDataSerializer();

        SaveObject(storedData, storedData.GetType().ToString(), formatter, slotIndex);
    }
    public static void SaveScriptableObjects<T>(List<T> scriptableObjects)
        where T : ScriptableObject
    {
        SaveScriptableObjects<T>(scriptableObjects, CurGameSlot);
    }
    public static void SaveScriptableObjects<T>(List<T> scriptableObjects, int slotIndex)
        where T : ScriptableObject
    {
        foreach (var SOToSave in scriptableObjects)
        {
            if (SOToSave != null)
            {
                SaveScriptableObject<T>(SOToSave, slotIndex);
            }
        }
    }
    public static void SaveScriptableObject<T>(T savedSO) where T : ScriptableObject
    {
        SaveScriptableObject<T>(savedSO, CurGameSlot);
    }
    public static void SaveScriptableObject<T>(T savedSO, int slotIndex) where T : ScriptableObject
    {
        SoSerializer formatter = new SoSerializer();

        SaveObject(savedSO, savedSO.name, formatter, slotIndex);
    }
    public static void SaveGeneralSettings(GeneralSettings generalSettings)
    {
        StoredDataSerializer formatter = new StoredDataSerializer();

        SaveObjectToRoot(generalSettings, "", typeof(GeneralSettings).ToString(), formatter);
    }
    public static List<T> LoadScriptableObjects<T>()
        where T : ScriptableObject
    {
        return LoadScriptableObjects<T>(CurGameSlot);
    }
    public static List<T> LoadScriptableObjects<T>(int slotIndex)
        where T : ScriptableObject
    {
        List<T> loadedSOs = new List<T>();

        string subfolderName = typeof(T).ToString() + "s";
        string directoryPath = Path.Combine(saveFolder, "GameSlot" + slotIndex, subfolderName);
        if (Directory.Exists(directoryPath))
        {
            string[] filesToLoad = Directory.GetFiles(directoryPath);

            foreach (var file in filesToLoad)
            {
                loadedSOs.Add((T)LoadDataByPath<T, ScriptableObject>(file, new SoSerializer()));
            }

            return loadedSOs;
        }
        else
        {
            return null;
        }
    }
    public static List<T> LoadStoredDatas<T>() where T : IStoredData
    {
        return LoadStoredDatas<T>(CurGameSlot);
    }
    public static List<T> LoadStoredDatas<T>(int slotIndex) where T : IStoredData
    {
        List<T> loadedSOs = new List<T>();

        string subfolderName = typeof(T).ToString() + "s";
        string directoryPath = Path.Combine(saveFolder, "GameSlot" + slotIndex, subfolderName);
        if (Directory.Exists(directoryPath))
        {
            string[] filesToLoad = Directory.GetFiles(directoryPath);

            foreach (var file in filesToLoad)
            {
                loadedSOs.Add((T)LoadDataByPath<T, IStoredData>(file, new StoredDataSerializer()));
            }

            return loadedSOs;
        }
        else
        {
            return null;
        }
    }
    public static GeneralSettings LoadGeneralSettings()
    {
        string filePath = Path.Combine(saveFolder, typeof(GeneralSettings).ToString() + EXTENSION);
        if (File.Exists(filePath))
        {
            return (GeneralSettings)LoadDataByPath<GeneralSettings, IStoredData>(filePath, new StoredDataSerializer());
        }

        return null;
    }

    private static D LoadDataByPath<T, D>(string filePath, ISerializer<D> formatter) where T : D
    {
        if (File.Exists(filePath))
        {
            FileStream stream = new FileStream(filePath, FileMode.Open);
            if (stream.Length <= 0)
            {
                stream.Close();
                File.Delete(filePath);
                return default;
            }
            else
            {
                D data = formatter.Deserialize(stream);

                stream.Close();

                return data;
            }
        }
        else
        {
            return default;
        }
    }
    private static void SaveObject<T>(T savedObject, string fileName, ISerializer<T> serializer)
    {
        SaveObject<T>(savedObject, fileName, serializer, CurGameSlot);
    }
    private static void SaveObject<T>(T savedObject, string fileName, ISerializer<T> serializer, int slotIndex)
    {
        string subfolderName = typeof(T).ToString() + "s";
        string objectsDirectory = Path.Combine("GameSlot" + slotIndex, subfolderName);

        SaveObjectToRoot<T>(savedObject, objectsDirectory, fileName, serializer);
    }
    private static void SaveObjectToRoot<T>(T savedObject, string directoryName, string fileName, ISerializer<T> serializer)
    {
        string objectsDirectory = Path.Combine(saveFolder, directoryName);
        if (!Directory.Exists(objectsDirectory))
        {
            Directory.CreateDirectory(objectsDirectory);
        }

        string path = Path.Combine(objectsDirectory, fileName + EXTENSION);
        FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
        serializer.Serialize(stream, savedObject);
        stream.Close();
    }


    private static int CurGameSlot => Storage.GeneralSettings.currentGameSlotIndex;
}