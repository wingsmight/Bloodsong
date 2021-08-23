using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCharacterNodeView : NodeView<CharacterPositionNode>
{
    [SerializeField] private CharactersView charactersView;


    public override void Act(DialogueGraphData dialogue, CharacterPositionNode nodeData)
    {
        var characterView = charactersView[nodeData.characterPosition];

        var nextNodes = dialogue.GetNextNodes(nodeData);
        NodeData nextNode;
        if (nextNodes.Count > 0)
        {
            nextNode = nextNodes[0];
        }
        else
        {
            var prevDialogue = dialogueParser.PrevDialogue;
            var nodes = prevDialogue.GetNextNodes(nodeData);

            if (nodes.Count > 0 && nodes[0] is StopNode)
            {
                nextNode = dialogue.GetNextNodes(dialogue.FirstNode)[0];
            }
            else
            {
                nextNode = nodes[0];
            }
        }

        if (nextNode is CharacterNode)
        {
            ProcessNext(dialogue, nodeData);
        }
        else if (nextNode is CharacterPositionNode)
        {
            ProcessNext(dialogue, nodeData);
        }
        else
        {
            Action processNextOnHidden = null;
            processNextOnHidden = () =>
            {
                characterView.OnHidden -= processNextOnHidden;

                ProcessNext(dialogue, nodeData);
            };
            characterView.OnHidden += processNextOnHidden;
        }

        characterView.Hide();
    }
    public void ActWithoutProcessNext(DialogueGraphData dialogue, CharacterPositionNode nodeData)
    {
        var characterView = charactersView[nodeData.characterPosition];

        characterView.Hide();
    }
}
