using System;
using UnityEngine;

[Serializable]
public class Phrase
{
    public Speaker speaker;
    [TextArea(8, 20)]
    public string text;
}
