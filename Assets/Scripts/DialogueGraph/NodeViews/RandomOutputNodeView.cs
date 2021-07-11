using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomOutputNodeView : NodeView<RandomOutputNode>
{
    public override void Act(DialogueGraphData dialogue, RandomOutputNode nodeData)
    {
        var outNodes = dialogue.GetNextNodes(nodeData);

        int randomNodeIndex = Random.Range(0, outNodes.Count);

        dialogueParser.ActNode(outNodes[randomNodeIndex]);
    }
}
