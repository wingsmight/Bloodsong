using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameDayData : IStoredData
{
    public string locationName;
    public Dictionary<CharacterView.Position, string> characters = new Dictionary<CharacterView.Position, string>();
    public string currentNodeGuid;
    public int currentStoryIndex;
    public int currentStoryPhraseIndex;
}
