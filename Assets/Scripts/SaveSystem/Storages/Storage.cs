using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Storage : MonoBehaviourSingleton<Storage>
{
    public const int SAVE_SLOTS_COUNT = 4;

    private const int MIN_SAVE_SLOT_INDEX = 0;


    private static List<Type> dataTypes;
    private static List<IStoredData>[] datas = new List<IStoredData>[SAVE_SLOTS_COUNT];
    private static GeneralSettings generalSettings;


    protected override void Awake()
    {
        base.Awake();

        LoadDatas();
        LoadGeneralSettings();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();

        SaveDatas();
        SaveGeneralSettings();
    }


    public static T GetData<T>() where T : IStoredData
    {
        return GetData<T>(CurSaveSlotIndex);
    }
    public static T GetData<T>(int slotIndex) where T : IStoredData
    {
        if (slotIndex < MIN_SAVE_SLOT_INDEX || slotIndex >= SAVE_SLOTS_COUNT)
            throw new Exception("Slot index is out of range");

        if (datas[slotIndex] == null)
            LoadDatas(slotIndex);

        return (T)datas[slotIndex].First(x => x.GetType() == typeof(T));
    }
    public static void DeleteData(int slotIndex)
    {
        datas[slotIndex] = null;
    }

    private static void LoadDatas()
    {
        if (CurSaveSlotIndex >= MIN_SAVE_SLOT_INDEX && CurSaveSlotIndex < SAVE_SLOTS_COUNT)
        {
            LoadDatas(CurSaveSlotIndex);
        }
    }
    private static void LoadDatas(int slotIndex)
    {
        if (slotIndex < MIN_SAVE_SLOT_INDEX || slotIndex >= SAVE_SLOTS_COUNT)
            return;

        datas[slotIndex] = GameFile.LoadStoredDatas<IStoredData>(slotIndex);
        if (datas[slotIndex] == null)
        {
            if (dataTypes == null)
            {
                var parentDataType = typeof(IStoredData);
                dataTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => parentDataType.IsAssignableFrom(p) && p != parentDataType && p.IsClass)
                    .ToList();
                dataTypes.Remove(typeof(GeneralSettings));
            }

            datas[slotIndex] = new List<IStoredData>();
            foreach (var dataType in dataTypes)
            {
                datas[slotIndex].Add(Activator.CreateInstance(dataType) as IStoredData);
            }
        }
    }
    private static void LoadGeneralSettings()
    {
        generalSettings = GameFile.LoadGeneralSettings();
        if (generalSettings == null)
        {
            generalSettings = Activator.CreateInstance<GeneralSettings>();
        }
    }
    private static void SaveDatas()
    {
        SaveDatas(CurSaveSlotIndex);
    }
    private static void SaveDatas(int slotIndex)
    {
        if (slotIndex < MIN_SAVE_SLOT_INDEX || slotIndex >= SAVE_SLOTS_COUNT)
            return;

        if (datas[slotIndex] != null)
        {
            GameFile.SaveStoredDatas<IStoredData>(datas[slotIndex], slotIndex);
        }
    }
    private static void SaveGeneralSettings()
    {
        if (generalSettings != null)
        {
            GameFile.SaveGeneralSettings(generalSettings);
        }
    }


    public static GeneralSettings GeneralSettings
    {
        get
        {
            if (generalSettings == null)
                LoadGeneralSettings();

            return generalSettings;
        }
    }

    private static int CurSaveSlotIndex => GeneralSettings.currentSaveSlotIndex;
}
