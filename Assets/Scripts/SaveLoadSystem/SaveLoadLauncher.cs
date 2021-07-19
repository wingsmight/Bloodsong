using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadLauncher : MonoBehaviourSingleton<SaveLoadLauncher>
{
    private List<IDataLoading> dataLoadings;
    private List<IDataLinking> dataLinkings;
    private List<IDataSaving> dataSavings;


    protected override void Awake()
    {
        dataLoadings = UnityEngineObjectExt.FindObjectsOfInterface<IDataLoading>();
        dataLinkings = UnityEngineObjectExt.FindObjectsOfInterface<IDataLinking>();
        dataSavings = UnityEngineObjectExt.FindObjectsOfInterface<IDataSaving>();

        Storage.LoadGeneralSettings();
    }
    private void OnDisable()
    {
        // auto saving on quiting
        //SaveDatas();

        Storage.SaveGeneralSettings();
    }


    public void LoadDatas()
    {
        foreach (var item in dataLoadings)
        {
            try
            {
                item?.LoadData();
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"LoadDatas() in {item} has produced {exception} at the 2nd try-catch block");
            }
        }
    }
    public void LinkDatas()
    {
        foreach (var item in dataLinkings)
        {
            item?.LinkData();
        }
    }
    public void SaveDatas()
    {
        // TODO move this to somewhere
        try
        {
            Storage.GetData<PlayerPreferences>().lastExitDate = new DateTimeData(DateTime.Now);
        }
        catch (Exception exception)
        {
            Debug.LogWarning($"SaveDatas() has produced {exception}  at the 1st try-catch block");
        }

        foreach (var item in dataSavings)
        {
            try
            {
                item?.SaveData();
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"SaveDatas() in {item} has produced {exception} at the 2nd try-catch block");
            }
        }

        Storage.SaveDatas();
    }
}
