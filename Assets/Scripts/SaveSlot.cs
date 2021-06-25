using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlot
{
    public DateTime lastOpenDate = DateTime.MinValue;
    public Sprite locationSprite;


    public SaveSlot()
    {

    }
    public SaveSlot(DateTime lastOpenDate, Sprite locationSprite) : this()
    {
        this.lastOpenDate = lastOpenDate;
        this.locationSprite = locationSprite;
    }


    public bool IsEmpty => lastOpenDate == DateTime.MinValue;
}
