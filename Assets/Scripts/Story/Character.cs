using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Character0", menuName = "Character")]
[Serializable]
public class Character : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private PositionSpriteDictionary sprites;


    public Sprite GetSprite(CharacterView.Position position)
    {
        return sprites[position];
    }


    public string Name => Localization.GetValue(name);
}
