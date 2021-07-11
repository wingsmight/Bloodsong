using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNodeView : NodeView<DialogueNode>
{
    [SerializeField] private TextTyping dialogueTyping;
    [SerializeField] private DialoguePanel communicationPanel;
    [SerializeField] private ResponsesPanel responsesPanel;


    public override void Act(DialogueGraphData dialogue, DialogueNode nodeData)
    {
        string text = ReplaceExposeds(dialogue, nodeData.dialogueText);

        dialogueTyping.Type(text);

        responsesPanel.Clear();
        responsesPanel.Enable();
        for (int i = 0; i < nodeData.responses.Count; i++)
        {
            var response = nodeData.responses[i];
            NodeData responseDestNode = dialogue.GetNextPortNodes(nodeData, response.text)[0];
            string responseText = ReplaceExposeds(dialogue, response.text);

            if (response is DialogueStatResponseData)
            {
                StatAdditive statAdditive = (response as DialogueStatResponseData).statAdditive;
                var dialogueResponse = new DialogueStatResponseData(responseText, statAdditive);

                responsesPanel.AddResponse(dialogueResponse, () =>
                {
                    dialogueParser.ActNode(responseDestNode);
                });
            }
            else if (response is DialogueStatsResponseData)
            {
                List<StatAdditive> statAdditives = (response as DialogueStatsResponseData).statAdditives;
                var dialogueResponse = new DialogueStatsResponseData(responseText, statAdditives);

                responsesPanel.AddResponse(dialogueResponse, () =>
                {
                    dialogueParser.ActNode(responseDestNode);
                });
            }
            else
            {
                var dialogueResponse = new DialogueResponseData(responseText);

                responsesPanel.AddResponse(dialogueResponse, () =>
                {
                    dialogueParser.ActNode(responseDestNode);
                });
            }
        }
    }
    public void Stop()
    {
        dialogueTyping.Stop();
        communicationPanel.StopConversation();
    }

    private string ReplaceExposeds(DialogueGraphData dialogue, string text)
    {
        foreach (var exposedProperty in dialogue.ExposedVariables)
        {
            text = text.Replace($"[{exposedProperty.Name}]", exposedProperty.Value);
        }
        return text;
    }
}
