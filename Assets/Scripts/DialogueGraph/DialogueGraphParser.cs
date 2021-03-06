using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueGraphParser : MonoBehaviour
{
    [SerializeField] private EntryNodeView entryNodeView;
    [SerializeField] private DialogueNodeView dialogueView;
    [SerializeField] private ShowCharacterNodeView showCharacterView;
    [SerializeField] private StartMonologueNodeView startMonologueView;
    [SerializeField] private StopNodeView stopNodeView;
    [SerializeField] private RandomOutputNodeView randomOutputNodeView;
    [SerializeField] private StartSplitMessageNodeView startSplitMessageNodeView;
    [SerializeField] private LocationNodeView locationNodeView;
    [SerializeField] private HideCharacterNodeView hideCharacterNodeView;
    [SerializeField] private PlayMusicNodeView playMusicNodeView;
    [SerializeField] private StopMusicNodeView stopMusicNodeView;
    [Space(12)]
    [SerializeField] private GameDayControl gameDayControl;


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

        gameDayControl.BranchNodes.Push(nodeData);

        if (nodeData is DialogueNode)
        {
            dialogueView.Act(currentDialogue, nodeData as DialogueNode);
        }
        else if (nodeData is EntryNode)
        {
            entryNodeView.Act(currentDialogue, nodeData as EntryNode);
        }
        else if (nodeData is CharacterNode)
        {
            showCharacterView.Act(currentDialogue, nodeData as CharacterNode);
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
        else if (nodeData is LocationNode)
        {
            locationNodeView.Act(currentDialogue, nodeData as LocationNode);
        }
        else if (nodeData is CharacterPositionNode)
        {
            hideCharacterNodeView.Act(currentDialogue, nodeData as CharacterPositionNode);
        }
        else if (nodeData is MusicNode)
        {
            playMusicNodeView.Act(currentDialogue, nodeData as MusicNode);
        }
        else if (nodeData is StopMusicNode)
        {
            stopMusicNodeView.Act(currentDialogue, nodeData as StopMusicNode);
        }
        else
        {
            throw new Exception();
        }
    }
    public void StopNode(NodeData nodeData)
    {
        if (nodeData is DialogueNode)
        {
            dialogueView.Stop();
        }
        else if (nodeData is EntryNode)
        {
            entryNodeView.Stop();
        }
        else if (nodeData is CharacterNode)
        {
            showCharacterView.Stop();
        }
        else if (nodeData is MonologueNode)
        {
            startMonologueView.Stop();
        }
        else if (nodeData is StopNode)
        {
            stopNodeView.Stop();
        }
        else if (nodeData is RandomOutputNode)
        {
            randomOutputNodeView.Stop();
        }
        else if (nodeData is SplitMessageNode)
        {
            startSplitMessageNodeView.Stop();
        }
        else if (nodeData is LocationNode)
        {
            locationNodeView.Stop();
        }
        else if (nodeData is CharacterPositionNode)
        {
            hideCharacterNodeView.Stop();
        }
        else if (nodeData is MusicNode)
        {
            playMusicNodeView.Stop();
        }
        else if (nodeData is StopMusicNode)
        {
            stopMusicNodeView.Stop();
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


    public DialogueGraphData CurrentDialogue { get => currentDialogue; set => currentDialogue = value; }
    public DialogueGraphData PrevDialogue => gameDayControl.PrevDialogue;
    public NodeData CurrentNode { get => currentNode; set => currentNode = value; }
}