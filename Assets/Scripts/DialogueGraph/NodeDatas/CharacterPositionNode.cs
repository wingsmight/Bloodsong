using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterPositionNode : NodeData
{
    public CharacterView.Position characterPosition;


    public CharacterPositionNode(string guid, Vector2 position, CharacterView.Position hidePosition)
        : base(guid, position)
    {
        this.characterPosition = hidePosition;
    }
}
