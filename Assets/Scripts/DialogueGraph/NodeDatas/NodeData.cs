using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NodeData
{
    public string guid;
    public Vector2 position;


    public NodeData(string guid, Vector2 position)
    {
        this.guid = guid;
        this.position = position;
    }


    public string GUID { get => guid; set => value = guid; }
    public Vector2 Position => position;
}
