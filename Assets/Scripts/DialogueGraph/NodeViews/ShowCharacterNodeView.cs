using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCharacterNodeView : NodeView<CharacterNode>
{
    [SerializeField] private DialoguePanel dialoguePanel;
    [SerializeField] private CharactersView visiterView;


    public override void Act(DialogueGraphData dialogue, CharacterNode nodeData)
    {
        visiterView.Show(nodeData.character, nodeData.characterPosition, nodeData.characterDirection);

        ProcessNext(dialogue, nodeData);
    }
}
