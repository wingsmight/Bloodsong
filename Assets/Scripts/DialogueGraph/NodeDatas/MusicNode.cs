using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MusicNode : NodeData
{
    public string name;


    public MusicNode(string guid, Vector2 position, string name) : base(guid, position)
    {
        this.name = name;
    }
}
