using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCharacterNodeView : NodeView<CharacterNode>
{
    [SerializeField] private DialoguePanel dialoguePanel;
    [SerializeField] private CharactersView characterView;


    public override void Act(DialogueGraphData dialogue, CharacterNode nodeData)
    {
        Character character = ScriptableObjectFinder.Get<Character>(nodeData.character.name);

        if (character == null || string.IsNullOrEmpty(character.name) || string.IsNullOrEmpty(character.Name))
        {
            characterView.Hide(nodeData.character.position);
        }
        else
        {
            characterView.Show(character, nodeData.character.position, nodeData.character.emotion, nodeData.direction);
        }

        ProcessNext(dialogue, nodeData);
    }
}
