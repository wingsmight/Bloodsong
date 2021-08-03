using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BranchNodesStack
{
    private List<NodeData> nodes = new List<NodeData>();


    public void Push(NodeData node)
    {
        if (nodes.Count > 0 && node == nodes.Last())
            return;

        nodes.Add(node);
    }
    public NodeData Pop()
    {
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
            for (int i = lastNodeIndex - 1; i > 0; i--)
            {
                var node = nodes[i];
                if (node is LocationNode)
                {
                    LocationNode locationNode = node as LocationNode;
                    if (locationNode.name == locationPeekNode.name)
                    {
                        return locationNode;
                    }
                }
            }

            //TODO IDK what to return
            return peekNode;
        }

        return peekNode;
    }
    public void CutToMilestone()
    {
        // Character
        var characterNodes = nodes.Where(x => x is CharacterNode).Cast<CharacterNode>().ToList();
        int positionCount = Enum.GetNames(typeof(CharacterView.Position)).Length;
        if (characterNodes.Count > 0)
        {
            for (int i = 0; i < positionCount; i++)
            {
                var positionedCharacters = characterNodes.Where(x => x.character.position == (CharacterView.Position)i).ToList();
                for (int j = 0; j < positionedCharacters.Count; j++)
                {
                    nodes.Remove(positionedCharacters[i]);
                }
            }
        }

        // Location
        var removedLocationNodes = nodes.Where(x => x is LocationNode).ToList();
        if (removedLocationNodes.Count > 0)
        {
            for (int i = 0; i < removedLocationNodes.Count - 1; i++)
            {
                nodes.Remove(removedLocationNodes[i]);
            }
        }
    }
    public static bool IsSpecificNode(NodeData node)
    {
        return node is CharacterNode
            || node is LocationNode;
    }
    public int Count => nodes.Count;
}
