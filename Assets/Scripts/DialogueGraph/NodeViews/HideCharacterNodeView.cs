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

        if (dialogue.GetNextNodes(nodeData)[0] is CharacterNode)
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
}
