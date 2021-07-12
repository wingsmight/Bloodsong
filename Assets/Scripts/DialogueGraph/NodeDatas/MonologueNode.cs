using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonologueNode : NodeData
{
    public List<string> texts;
    public string speakerName;


    public MonologueNode(string guid, Vector2 position, List<string> texts, string speakerName)
        : base(guid, position)
    {
        this.texts = texts;
        this.speakerName = speakerName;
    }
    public MonologueNode(string guid, Vector2 position, List<string> texts, Character character)
        : this(guid, position, texts, character.Name)
    {
        this.texts = texts;
        this.speakerName = character.Name;
    }
    public MonologueNode(MonologueNode monologueNode)
        : this(monologueNode.guid, monologueNode.position, monologueNode.texts, monologueNode.speakerName)
    {

    }
}
