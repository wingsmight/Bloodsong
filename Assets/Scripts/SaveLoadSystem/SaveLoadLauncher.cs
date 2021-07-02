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

        LoadDatas();
        LinkDatas();
    }
    private void OnDisable()
    {
        SaveDatas();
    }

    public void LoadDatas()
    {
        Storage.LoadGeneralSettings();
        Storage.LoadDatas();

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
        catch { }

        foreach (var item in dataSavings)
        {
            try
            {
                item?.SaveData();
            }
            catch { }
        }

        Storage.SaveDatas();
        Storage.SaveGeneralSettings();
    }
}
