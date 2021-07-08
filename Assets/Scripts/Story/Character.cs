using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Character0", menuName = "Character")]
[Serializable]
public class Character : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private Sprite sprite;


    public string Name => name;
    public Sprite Sprite => sprite;
}
