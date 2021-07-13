using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameDayData : IStoredData
{
    public string locationName;
    public PositionCharacterNameDictionary characters = new PositionCharacterNameDictionary();
    public int currentStoryIndex;
    public string currentNodeGuid;
}
