using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCharacterNodeView : NodeView<CharacterNode>
{
    [SerializeField] private CharactersView charactersView;


    public override void Act(DialogueGraphData dialogue, CharacterNode nodeData)
    {
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

        var characterView = charactersView[nodeData.character.position];
        Character character = ScriptableObjectFinder.Get<Character>(nodeData.character.name);

        if (nextNode is CharacterNode)
        {
            ProcessNext(dialogue, nodeData);
        }
        else
        {
            Action processNextOnShown = null;
            processNextOnShown = () =>
            {
                characterView.OnShown -= processNextOnShown;

                ProcessNext(dialogue, nodeData);
            };
            characterView.OnShown += processNextOnShown;
        }

        characterView.Show(character, nodeData.character.emotion, nodeData.direction);
    }
    public void ActWithoutProcessNext(DialogueGraphData dialogue, CharacterNode nodeData)
    {
        var characterView = charactersView[nodeData.character.position];
        Character character = ScriptableObjectFinder.Get<Character>(nodeData.character.name);

        characterView.Show(character, nodeData.character.emotion, nodeData.direction);
    }
}
