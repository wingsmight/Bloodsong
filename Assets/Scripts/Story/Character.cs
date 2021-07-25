using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Character0", menuName = "Character")]
[Serializable]
public class Character : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private CharacterSpriteDictionary sprites;


    public Sprite GetSprite(Emotion emotion)
    {
        try
        {
            return sprites[emotion];
        }
        catch
        {
            Debug.LogError($"{emotion} emotion is missed for {name} character");
            return null;
        }
    }


    public string Name => Localization.GetValue(name);


    public enum Emotion
    {
        Usual,
        Happy,
        Sad,
        Lust,
        Severe,
        Surprised,
    }
}
