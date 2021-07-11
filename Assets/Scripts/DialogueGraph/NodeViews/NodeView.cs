using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NodeView<T> : MonoBehaviour
    where T : NodeData
{
    [SerializeField] protected DialogueGraphParser dialogueParser;


    public abstract void Act(DialogueGraphData dialogue, T nodeData);

    protected void ProcessNext(DialogueGraphData dialogue, NodeData nodeData)
    {
        var nodes = dialogue.GetNextNodes(nodeData);
        foreach (var node in nodes)
        {
            dialogueParser.ActNode(node);
        }

        if(nodes.Count <= 0)
        {
            dialogueParser.Stop(dialogue);
        }
    }


    public System.Type DataType => typeof(T);
}
