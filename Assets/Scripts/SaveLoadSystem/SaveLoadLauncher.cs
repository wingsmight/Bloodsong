using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadLauncher : MonoBehaviour
{
    [SerializeField] private bool isAutoSave = false;


    private List<IDataLoading> dataLoadings;
    private List<IDataLinking> dataLinkings;
    private List<IDataSaving> dataSavings;


    private void Awake()
    {
        dataLoadings = UnityEngineObjectExt.FindObjectsOfInterface<IDataLoading>();
        dataLinkings = UnityEngineObjectExt.FindObjectsOfInterface<IDataLinking>();
        dataSavings = UnityEngineObjectExt.FindObjectsOfInterface<IDataSaving>();

        LoadDatas();
        LinkDatas();
    }
    private void OnDisable()
    {
        if (isAutoSave)
        {
            SaveDatas();
        }
    }


    public void SaveDatas()
    {
        foreach (var item in dataSavings)
        {
            try
            {
                item?.SaveData();
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"SaveDatas() in {item} has produced {exception}");
            }
        }
    }


    private void LoadDatas()
    {
        foreach (var item in dataLoadings)
        {
            try
            {
                item?.LoadData();
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"LoadDatas() in {item} has produced {exception}");
            }
        }
    }
    private void LinkDatas()
    {
        foreach (var item in dataLinkings)
        {
            try
            {
                item?.LinkData();
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"LinkDatas() in {item} has produced {exception}");
            }
        }
    }


    public bool IsAutoSave => isAutoSave;
}
