using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameDayData : IStoredData
{
    public Location lastLocation = new Location("VillageFarView");
    public int currentStoryIndex;
    [SerializeReference] public NodeData currentNode;
}
