using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GeneralSettings : IStoredData
{
    public int lastGameSlot = -1;
    public int currentGameSlotIndex = -1;
    public bool[] isSlotUseds = new bool[Storage.SAVE_SLOTS_COUNT];
}
