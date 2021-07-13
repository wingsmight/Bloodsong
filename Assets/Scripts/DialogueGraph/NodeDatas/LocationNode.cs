using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LocationNode : NodeData
{
    public string name;


    public LocationNode(string guid, Vector2 position, string name) : base(guid, position)
    {
        this.name = name;
    }
}
