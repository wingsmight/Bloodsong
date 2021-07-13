using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveSlot
{
    public DateTime lastExitDate = DateTime.MinValue;
    public Sprite locationSprite = null;


    public SaveSlot()
    {

    }
    public SaveSlot(GameSlot gameSlot)
    {
        if (gameSlot.IsUsed)
        {
            lastExitDate = Storage.GetData<PlayerPreferences>(gameSlot.Index).lastExitDate.Date;
            locationSprite = new Location(Storage.GetData<GameDayData>(gameSlot.Index).locationName).Sprite;
        }
    }
    public SaveSlot(DateTime lastOpenDate, Sprite locationSprite) : this()
    {
        this.lastExitDate = lastOpenDate;
        this.locationSprite = locationSprite;
    }


    public bool IsEmpty => lastExitDate == DateTime.MinValue;
}
