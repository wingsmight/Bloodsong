using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueNode : NodeData
{
    public string dialogueText;
    public string speakerName;
    [SerializeReference] public List<DialogueResponseData> responses = new List<DialogueResponseData>();



    public DialogueNode(string guid, Vector2 position, string dialogueText, List<DialogueResponseData> responses, Character character)
        : base(guid, position)
    {
        this.dialogueText = dialogueText;
        this.responses = responses;
        this.speakerName = character.Name;
    }
}