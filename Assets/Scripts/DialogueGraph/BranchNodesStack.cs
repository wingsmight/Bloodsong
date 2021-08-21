using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BranchNodesStack
{
    private static readonly Type[] specificTypes =
    {
        typeof(CharacterNode),
        typeof(CharacterPositionNode),
        typeof(LocationNode)
    };
    private static readonly Type[] exceptionTypes =
    {
        typeof(EntryNode),
        typeof(StopNode)
    };
    private static readonly Type[] finishTypes =
    {
        typeof(DialogueNode),
    };


    [SerializeReference] [SerializeField] private List<NodeData> nodes = new List<NodeData>();


    public void Push(NodeData node)
    {
        if (IsEqualsLast(node) || IsExceptionType(node.GetType()))
            return;

        if (IsFinishNode(node))
        {
            Clear();
            return;
        }

        nodes.Add(node);
    }
    public NodeData Pop()
    {
        if (nodes.Count <= 0)
            return null;

        int lastNodeIndex = nodes.Count - 1;
        var peekNode = nodes[lastNodeIndex];
        nodes.RemoveAt(lastNodeIndex);

        // Character
        if (peekNode is CharacterNode)
        {
            CharacterNode characterPeekNode = peekNode as CharacterNode;
            for (int i = lastNodeIndex - 1; i > 0; i--)
            {
                var node = nodes[i];
                if (node is CharacterNode)
                {
                    CharacterNode characterNode = node as CharacterNode;
                    if (characterNode.character.position == characterPeekNode.character.position)
                    {
                        return characterNode;
                    }
                }
            }

            // TODO why do you pass GUID & position of Peek node?
            return new CharacterPositionNode(characterPeekNode.guid, characterPeekNode.position, characterPeekNode.character.position);
        }
        // Location
        else if (peekNode is LocationNode)
        {
            LocationNode locationPeekNode = peekNode as LocationNode;
            for (int i = lastNodeIndex - 1; i >= 0; i--)
            {
                var node = nodes[i];
                if (node is LocationNode)
                {
                    return node;
                }
            }

            //TODO IDK what to return
            return peekNode;
        }

        return peekNode;
    }
    public void Clear()
    {
        nodes.Clear();
    }
    public static bool IsSpecificNode(NodeData node)
    {
        var nodeType = node.GetType();

        foreach (var specificType in specificTypes)
        {
            if (nodeType == specificType)
            {
                return true;
            }
        }

        return false;
    }

    private bool IsFinishNode(NodeData node)
    {
        var nodeType = node.GetType();

        foreach (var specificType in finishTypes)
        {
            if (nodeType == specificType)
            {
                return true;
            }
        }

        return false;
    }
    private bool IsExceptionType(Type type)
    {
        foreach (var exceptionType in exceptionTypes)
        {
            if (type == exceptionType)
            {
                return true;
            }
        }

        return false;
    }
    private bool IsEqualsLast(NodeData node)
    {
        return nodes.Count > 0 && node == nodes.Last();
    }


    public NodeData First => Count > 0 ? nodes[0] : null;
    public NodeData Last => Count > 0 ? nodes[nodes.Count - 1] : null;
    public int Count => nodes.Count;
}
