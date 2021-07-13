using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GeneralSettings : IStoredData
{
    public int lastGameSlot = -1;
    [NonSerialized] public int currentGameSlotIndex = -1;
    public bool[] isSlotUseds = new bool[Storage.SAVE_SLOTS_COUNT];

    // options
    public int[] resolution = new int[] { 1920, 1080 };
    public bool isFullscreen = true;
    public float volume = 1.0f;
    public float testTypingSpeed = 0.01f;
}
