using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCharacterNodeView : NodeView<CharacterNode>
{
    [SerializeField] private CharactersView charactersView;


    public override void Act(DialogueGraphData dialogue, CharacterNode nodeData)
    {
        var characterView = charactersView[nodeData.character.position];

        Character character = ScriptableObjectFinder.Get<Character>(nodeData.character.name);
        if (character == null || string.IsNullOrEmpty(character.name) || string.IsNullOrEmpty(character.Name))
        {
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
        else
        {
            Action processNextOnShown = null;
            processNextOnShown = () =>
            {
                characterView.OnShown -= processNextOnShown;

                ProcessNext(dialogue, nodeData);
            };
            characterView.OnShown += processNextOnShown;

            characterView.Show(character, nodeData.character.emotion, nodeData.direction);
        }
    }
}
