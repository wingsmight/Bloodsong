using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SplitMessageNode : NodeData
{
    public List<string> text = new List<string>();


    public SplitMessageNode(string guid, Vector2 position, List<string> text) : base(guid, position)
    {
        this.text = text;
    }
}
