using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCharacterNodeView : NodeView<EnterCharacterNode>
{
    [SerializeField] private DialoguePanel dialoguePanel;
    [SerializeField] private CharacterView visiterView;


    public override void Act(DialogueGraphData dialogue, EnterCharacterNode nodeData)
    {
        dialoguePanel.AddActionAfterStop(() => visiterView.Show(ScriptableObjectFinder.Get<Character>(nodeData.visiterName)));

        ProcessNext(dialogue, nodeData);
    }
}
