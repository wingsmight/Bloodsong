using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Storage
{
    public const int SAVE_SLOTS_COUNT = 3;

    private const int MIN_SAVE_SLOT_INDEX = 0;


    private static List<Type> dataTypes;
    private static List<IStoredData>[] datas = new List<IStoredData>[SAVE_SLOTS_COUNT];
    private static GeneralSettings generalSettings;


    public static void LoadDatas()
    {
        if (CurGameSlot >= MIN_SAVE_SLOT_INDEX && CurGameSlot < SAVE_SLOTS_COUNT)
        {
            LoadDatas(CurGameSlot);
        }
    }
    public static void LoadDatas(int slotIndex)
    {
        datas[slotIndex] = SaveSystem.LoadStoredDatas<IStoredData>(slotIndex);
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
    public static void LoadGeneralSettings()
    {
        generalSettings = SaveSystem.LoadGeneralSettings();
        if (generalSettings == null)
        {
            generalSettings = Activator.CreateInstance<GeneralSettings>();
        }
    }
    public static void SaveDatas()
    {
        SaveDatas(CurGameSlot);
    }
    public static void SaveDatas(int slotIndex)
    {
        if (slotIndex < MIN_SAVE_SLOT_INDEX || slotIndex >= SAVE_SLOTS_COUNT)
            return;

        if (datas[slotIndex] != null)
        {
            SaveSystem.SaveStoredDatas<IStoredData>(datas[slotIndex], slotIndex);
        }
    }
    public static void SaveGeneralSettings()
    {
        if (generalSettings != null)
        {
            SaveSystem.SaveGeneralSettings(generalSettings);
        }
    }


    public static T GetData<T>() where T : IStoredData
    {
        return GetData<T>(CurGameSlot);
    }
    public static T GetData<T>(int slotIndex) where T : IStoredData
    {
        if (slotIndex < MIN_SAVE_SLOT_INDEX || slotIndex >= SAVE_SLOTS_COUNT)
            throw new Exception("Slot index is out of range");

        if (datas[slotIndex] == null)
            LoadDatas(slotIndex);

        return (T)datas[slotIndex].First(x => x.GetType() == typeof(T));
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

    private static int CurGameSlot => GeneralSettings.currentGameSlotIndex;
}
