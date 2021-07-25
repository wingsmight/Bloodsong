using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterNode : NodeData
{
    public CharacterProperty character;
    public CharacterView.Direction direction;


    public CharacterNode(string guid, Vector2 position, CharacterProperty character, CharacterView.Direction direction)
        : base(guid, position)
    {
        this.character = character;
        this.direction = direction;
    }
}
