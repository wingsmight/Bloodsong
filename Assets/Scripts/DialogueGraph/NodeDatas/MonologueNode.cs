using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonologueNode : NodeData
{
    public string text;


    public MonologueNode(string guid, Vector2 position, string text) : base(guid, position)
    {
        this.text = text;
    }
}
