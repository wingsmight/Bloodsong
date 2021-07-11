using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnterCharacterNode : NodeData
{
    public string visiterName;


    public EnterCharacterNode(string guid, Vector2 position, string visiterName) : base(guid, position)
    {
        this.visiterName = visiterName;
    }
}
