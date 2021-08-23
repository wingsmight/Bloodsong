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


    public BranchNodesStack()
    {

    }
    public BranchNodesStack(BranchNodesStack branchNodesStack)
    {
        nodes = new List<NodeData>(branchNodesStack.nodes);
    }


    public void Push(NodeData node)
    {
        if (IsEqualsLast(node) || IsExceptionType(node.GetType()))
            return;

        if (IsFinishNode(node))
        {
            Clear();
            return;
        }

        Debug.Log(node + " was pushed");

        nodes.Add(node);
    }
    public NodeData Pop()
    {
        if (nodes.Count <= 0)
            return null;

        int lastNodeIndex = nodes.Count - 1;
        var peekNode = nodes[lastNodeIndex];
        nodes.RemoveAt(lastNodeIndex);
        lastNodeIndex--;

        // Character
        if (peekNode is CharacterNode) // Show character
        {
            CharacterNode characterPeekNode = peekNode as CharacterNode;
            for (int i = lastNodeIndex; i >= 0; i--)
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
                else if (node is CharacterPositionNode) // Hide character
                {
                    return new CharacterPositionNode(characterPeekNode.guid, characterPeekNode.position, characterPeekNode.character.position);
                }
            }

            // TODO why do you pass GUID & position of Peek node?
            return new CharacterPositionNode(characterPeekNode.guid, characterPeekNode.position, characterPeekNode.character.position);
        }
        if (peekNode is CharacterPositionNode) // Hide character
        {
            CharacterPositionNode hiddenCharacterPositionPeekNode = peekNode as CharacterPositionNode;
            for (int i = lastNodeIndex; i >= 0; i--)
            {
                var node = nodes[i];
                if (node is CharacterNode)
                {
                    CharacterNode characterNode = node as CharacterNode;
                    if (characterNode.character.position == hiddenCharacterPositionPeekNode.characterPosition)
                    {
                        return characterNode;
                    }
                }
            }

            return null;
        }
        // Location
        else if (peekNode is LocationNode) // Show location
        {
            LocationNode locationPeekNode = peekNode as LocationNode;
            for (int i = lastNodeIndex; i >= 0; i--)
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
        var lastLocation = nodes.LastOrDefault(x => x is LocationNode);
        List<CharacterNode> lastCharacters = new List<CharacterNode>();
        int lastNodeIndex = nodes.Count - 1;
        for (int i = 0; i < Enum.GetValues(typeof(CharacterView.Position)).Length; i++)
        {
            CharacterView.Position position = (CharacterView.Position)i;
            for (int nodeIndex = lastNodeIndex; nodeIndex >= 0; nodeIndex--)
            {
                var node = nodes[nodeIndex];
                if (node is CharacterPositionNode) // Hide character node
                {
                    CharacterPositionNode characterNode = node as CharacterPositionNode;
                    if (characterNode.characterPosition == position)
                    {
                        break;
                    }
                }
                else if (node is CharacterNode) // Show character node
                {
                    CharacterNode characterNode = node as CharacterNode;
                    if (characterNode.character.position == position)
                    {
                        lastCharacters.Add(characterNode);
                        break;
                    }
                }
            }

        }
        // foreach (var characterView in characterViews)
        // {
        //     if (characterView.IsShowing)
        //     {
        //         CharacterNode lastCharacter = nodes.LastOrDefault(x => x is CharacterNode
        //             && (x as CharacterNode).character.position == characterView.CharacterProperty.position) as CharacterNode;

        //         if (lastCharacter != null)
        //         {
        //             lastCharacters.Add(lastCharacter);
        //         }
        //     }
        // }

        nodes.Clear();

        if (lastLocation != null)
        {
            nodes.Add(lastLocation);
        }
        nodes.AddRange(lastCharacters);
    }
    public NodeData FirstOf<T>()
    {
        return nodes.FirstOrDefault(x => x is T);
    }
    public bool IsPrevNodesSpecific()
    {
        return nodes.Take(nodes.Count - 1).All(x => IsSpecificNode(x));
    }

    public static bool IsSpecificNode(NodeData node)
    {
        if (node is null)
            return false;

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
