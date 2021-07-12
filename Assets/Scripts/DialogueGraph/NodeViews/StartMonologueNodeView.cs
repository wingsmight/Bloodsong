using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class StartMonologueNodeView : NodeView<MonologueNode>
{
    private const float DELAY_AFTER_STOP = 0.15f;


    [SerializeField] private MonologuePanel monologuePanel;


    public override void Act(DialogueGraphData dialogue, MonologueNode nodeData)
    {
        Character speaker = ScriptableObjectFinder.Get(nodeData.speakerName, typeof(Character)) as Character;
        monologuePanel.Show(nodeData.texts[0], speaker);
        monologuePanel.AddActionAfterHide(() =>
        {
            if (nodeData.texts.Count > 1)
            {
                List<string> nextTexts = new List<string>(nodeData.texts);
                nextTexts.RemoveAt(0);
                ProcessWithDelay(new MonologueNode(nodeData.guid, nodeData.position, nextTexts, nodeData.speakerName), DELAY_AFTER_STOP);
            }
            else
            {
                ProcessNextWithDelay(dialogue, nodeData, DELAY_AFTER_STOP);
            }
        });
    }
}
