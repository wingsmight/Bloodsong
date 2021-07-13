using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterNode : NodeData
{
    public Character character;
    public CharacterView.Position characterPosition;
    public CharacterView.Direction characterDirection;


    public CharacterNode(string guid, Vector2 position, Character character, CharacterView.Position characterPosition, CharacterView.Direction characterDirection)
        : base(guid, position)
    {
        this.character = character;
        this.characterPosition = characterPosition;
        this.characterDirection = characterDirection;
    }
}
