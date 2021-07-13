using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueGraphParser : MonoBehaviour
{
    [SerializeField] private EntryNodeView entryNodeView;
    [SerializeField] private DialogueNodeView dialogueView;
    [SerializeField] private EnterCharacterNodeView enterCharacterView;
    [SerializeField] private StartMonologueNodeView startMonologueView;
    [SerializeField] private StopNodeView stopNodeView;
    [SerializeField] private RandomOutputNodeView randomOutputNodeView;
    [SerializeField] private StartSplitMessageNodeView startSplitMessageNodeView;


    private DialogueGraphData currentDialogue;
    private NodeData currentNode;


#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Skip(currentDialogue);
        }
    }
#endif


    public void Parse(DialogueGraphData dialogue)
    {
        Parse(dialogue, currentDialogue.FirstNode);
    }
    public void Parse(DialogueGraphData dialogue, string startNodeGUID)
    {
        Parse(dialogue, dialogue.GetNodeByGUID(startNodeGUID));
    }
    public void Parse(DialogueGraphData dialogue, NodeData startNode)
    {
        currentDialogue = dialogue;

        if (startNode == null)
        {
            startNode = dialogue.FirstNode;
        }
        ActNode(startNode);
    }
    public void ActNode(NodeData nodeData)
    {
        currentNode = nodeData;

        if (nodeData is DialogueNode)
        {
            dialogueView.Act(currentDialogue, nodeData as DialogueNode);
        }
        else if (nodeData is EntryNode)
        {
            entryNodeView.Act(currentDialogue, nodeData as EntryNode);
        }
        else if (nodeData is EnterCharacterNode)
        {
            enterCharacterView.Act(currentDialogue, nodeData as EnterCharacterNode);
        }
        else if (nodeData is MonologueNode)
        {
            startMonologueView.Act(currentDialogue, nodeData as MonologueNode);
        }
        else if (nodeData is StopNode)
        {
            stopNodeView.Act(currentDialogue, nodeData as StopNode);
        }
        else if (nodeData is RandomOutputNode)
        {
            randomOutputNodeView.Act(currentDialogue, nodeData as RandomOutputNode);
        }
        else if (nodeData is SplitMessageNode)
        {
            startSplitMessageNodeView.Act(currentDialogue, nodeData as SplitMessageNode);
        }
        else
        {
            throw new Exception();
        }
    }
    public void Stop(DialogueGraphData dialogue)
    {
        stopNodeView.Act();
    }
    public void Skip(DialogueGraphData dialogue)
    {
        if (dialogue == null)
            return;

        var node = dialogue.FirstNode;

        List<NodeData> nextnodes = dialogue.GetNextNodes(node);
        while (nextnodes.Count > 0)
        {
            node = nextnodes[0];
            ActNode(node);

            nextnodes = dialogue.GetNextNodes(node);
        }
    }


    public NodeData CurrentNode => currentNode;
}