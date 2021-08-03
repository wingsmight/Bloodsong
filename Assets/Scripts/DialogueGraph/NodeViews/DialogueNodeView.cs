using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueNodeView : NodeView<DialogueNode>
{
    [SerializeField] private DialoguePanel communicationPanel;
    [SerializeField] private ChoiceView choiceView;


    public override void Act(DialogueGraphData dialogue, DialogueNode nodeData)
    {
        var choices = new List<Choice>();
        var actions = new List<UnityAction>();

        for (int i = 0; i < nodeData.responses.Count; i++)
        {
            var response = nodeData.responses[i];

            choices.Add(new Choice(response.text));

            NodeData responseDestNode = dialogue.GetNextPortNodes(nodeData, response.text)[0];
            if (response is DialogueStatResponseData)
            {
                StatAdditive statAdditive = (response as DialogueStatResponseData).statAdditive;
                var dialogueResponse = new DialogueStatResponseData(response.text, statAdditive);
            }
            else if (response is DialogueStatsResponseData)
            {
                List<StatAdditive> statAdditives = (response as DialogueStatsResponseData).statAdditives;
                var dialogueResponse = new DialogueStatsResponseData(response.text, statAdditives);
            }
            else
            {
                var dialogueResponse = new DialogueResponseData(response.text);
            }

            actions.Add(() =>
            {
                communicationPanel.Hide();
                dialogueParser.ActNode(responseDestNode);
            });
        }

        communicationPanel.Show(nodeData.dialogueText, nodeData.speakerName, new ChoiceData(choices), actions);
    }
    public override void Stop()
    {
        communicationPanel.Reset();
        communicationPanel.Hide();
    }
}
