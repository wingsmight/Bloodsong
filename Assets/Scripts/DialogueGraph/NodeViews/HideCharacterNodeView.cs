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
        var nextNode = dialogue.GetNextNodes(nodeData)[0];

        if (nextNode is CharacterNode)
        {
            ProcessNext(dialogue, nodeData);
        }
        else if (dialogue.GetNextNodes(nodeData)[0] is CharacterPositionNode)
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
        var nextNode = dialogue.GetNextNodes(nodeData)[0];

        if (nextNode is CharacterNode)
        {
            ProcessNext(dialogue, nodeData);
        }
        else if (dialogue.GetNextNodes(nodeData)[0] is CharacterPositionNode)
        {
            ProcessNext(dialogue, nodeData);
        }

        characterView.Hide();
    }
}
