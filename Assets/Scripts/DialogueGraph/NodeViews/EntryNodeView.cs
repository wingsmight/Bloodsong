using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryNodeView : NodeView<EntryNode>
{
    public override void Act(DialogueGraphData dialogue, EntryNode nodeData)
    {
        var secondNodes = dialogue.GetNextNodes(nodeData);
        dialogueParser.ActNode(secondNodes[0]);
    }
}
