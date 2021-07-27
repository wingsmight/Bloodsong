using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCharacterNodeView : NodeView<CharacterPositionNode>
{
    [SerializeField] private DialoguePanel dialoguePanel;
    [SerializeField] private CharactersView characterView;


    public override void Act(DialogueGraphData dialogue, CharacterPositionNode nodeData)
    {
        characterView.Hide(nodeData.characterPosition);

        ProcessNext(dialogue, nodeData);
    }
}
