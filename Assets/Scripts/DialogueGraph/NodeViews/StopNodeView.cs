using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopNodeView : NodeView<NodeData>
{
    [SerializeField] private MonologuePanel monologuePanel;
    [SerializeField] private DialoguePanel dialoguePanel;


    public void Act()
    {
        monologuePanel.Hide();
        dialoguePanel.Hide();
    }

    public override void Act(DialogueGraphData dialogue, NodeData nodeData)
    {
        Act();
    }
}

