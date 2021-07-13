using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameDayData : IStoredData
{
    public Location lastLocation = new Location("VillageFarView");
    public int currentStoryIndex;
    public string currentNodeGuid;
}
