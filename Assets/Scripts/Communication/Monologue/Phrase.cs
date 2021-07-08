using System;
using UnityEngine;

[Serializable]
public class Phrase
{
    public Character speaker;
    [TextArea(8, 20)] public string text;
}
