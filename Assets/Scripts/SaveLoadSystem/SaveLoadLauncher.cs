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
        //Storage.LoadDatas(); // no matter: the CurGameSlotIndex = -1 on Awake();

        // idkidkidkidkidkidkidkidkidkidkidkidk
        // if (Storage.GeneralSettings.currentGameSlotIndex != -1)
        // {
        //     LoadDatas();
        //     LinkDatas();
        // }
    }
    // auto saving on quiting
    // private void OnDisable()
    // {
    //     SaveDatas();
    // }
    private void OnDisable()
    {
        Storage.GetData<PlayerPreferences>().lastExitDate = new DateTimeData(DateTime.Now); // KOSTIIILYLyil
    }

    public void LoadDatas()
    {
        foreach (var item in dataLoadings)
        {
            item?.LoadData();
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
        Storage.SaveGeneralSettings();
    }
}
