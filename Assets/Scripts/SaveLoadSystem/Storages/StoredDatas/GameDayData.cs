using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameDayData : IStoredData
{
    public string locationName;
    public List<CharacterProperty> characters = new List<CharacterProperty>();
    [SerializeReference] public BranchNodesStack branchNodes = new BranchNodesStack();
    public string currentNodeGuid;
    public int currentStoryIndex;
    public int currentStoryPhraseIndex;
}
