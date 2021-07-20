using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GeneralSettings : IStoredData
{
    // slots
    public int lastSaveSlotIndex = -1;
    public int currentSaveSlotIndex = -1;
    [SerializeField] private SaveSlot[] saveSlots = null;

    // options
    public int[] resolution = null;
    public bool isFullscreen = true;
    public float volume = 1.0f;
    public float testTypingSpeed = 0.01f;


    public SaveSlot CurrentSaveSlot => SaveSlots[currentSaveSlotIndex];
    public SaveSlot[] SaveSlots
    {
        get
        {
            if (saveSlots == null || saveSlots.Length == 0)
            {
                saveSlots = new SaveSlot[Storage.SAVE_SLOTS_COUNT];
                for (int i = 0; i < saveSlots.Length; i++)
                {
                    saveSlots[i] = new SaveSlot(i);
                }
            }

            return saveSlots;
        }
    }
}
