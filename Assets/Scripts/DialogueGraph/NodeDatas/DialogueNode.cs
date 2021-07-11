using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueNode : NodeData
{
    public string dialogueText;
    [SerializeReference] public List<DialogueResponseData> responses = new List<DialogueResponseData>();



    public DialogueNode (string guid, Vector2 position, string dialogueText, List<DialogueResponseData> responses) : base(guid, position)
    {
        this.dialogueText = dialogueText;
        this.responses = responses;
    }
}