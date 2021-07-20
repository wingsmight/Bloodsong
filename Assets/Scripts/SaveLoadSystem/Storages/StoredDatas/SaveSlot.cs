using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveSlot
{
    public int index = -1;
    public bool isUsed = false;
    public Location lastLocation = null;
    public DateTimeData lastExitDate = null;


    public SaveSlot(int index)
    {
        this.index = index;
    }
    public SaveSlot(int index, DateTimeData lastExitDate, Location lastLocation) : this(index)
    {
        isUsed = (lastExitDate != null && lastLocation != null);

        if (isUsed)
        {
            this.lastExitDate = lastExitDate;
            this.lastLocation = lastLocation;
        }
    }
    public SaveSlot(int index, DateTime lastExitDate, Sprite locationSprite) :
        this(index,
            new DateTimeData(lastExitDate),
            new Location(Storage.GetData<GameDayData>(index).locationName))
    {

    }
}
