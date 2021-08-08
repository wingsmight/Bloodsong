using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCharacterNodeView : NodeView<CharacterNode>
{
    [SerializeField] private CharactersView charactersView;


    public override void Act(DialogueGraphData dialogue, CharacterNode nodeData)
    {
        var nextNode = dialogue.GetNextNodes(nodeData)[0];

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
}
