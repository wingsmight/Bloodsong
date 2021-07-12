﻿using System.Collections;
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

        if (nodes.Count <= 0)
        {
            dialogueParser.Stop(dialogue);
        }
    }
    protected void Process(NodeData nodeData)
    {
        dialogueParser.ActNode(nodeData);
    }
    protected void ProcessNextWithDelay(DialogueGraphData dialogue, NodeData nodeData, float secondsDelay)
    {
        StartCoroutine(ProcessNextWithDelayRoutine(dialogue, nodeData, secondsDelay));
    }
    protected void ProcessWithDelay(NodeData nodeData, float secondsDelay)
    {
        StartCoroutine(ProcessWithDelayRoutine(nodeData, secondsDelay));
    }

    private IEnumerator ProcessNextWithDelayRoutine(DialogueGraphData dialogue, NodeData nodeData, float secondsDelay)
    {
        yield return new WaitForSeconds(secondsDelay);

        ProcessNext(dialogue, nodeData);
    }
    private IEnumerator ProcessWithDelayRoutine(NodeData nodeData, float secondsDelay)
    {
        yield return new WaitForSeconds(secondsDelay);

        Process(nodeData);
    }


    public System.Type DataType => typeof(T);
}
