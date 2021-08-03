using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterProperty
{
    public string name;
    public CharacterView.Position position;
    public Character.Emotion emotion;


    public CharacterProperty(string name, CharacterView.Position position, Character.Emotion emotion)
    {
        this.name = name;
        this.position = position;
        this.emotion = emotion;
    }
}
