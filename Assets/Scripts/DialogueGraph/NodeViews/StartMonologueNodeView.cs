using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMonologueNodeView : NodeView<MonologueNode>
{
    private const float DELAY_AFTER_STOP = 0.15f;

    [SerializeField] private MonologuePanel monologuePanel;


    public override void Act(DialogueGraphData dialogue, MonologueNode nodeData)
    {
        monologuePanel.StartConversation(nodeData.text);
        monologuePanel.AddActionAfterStop(() =>
        {
            ProcessNextWithDelay(dialogue, nodeData, DELAY_AFTER_STOP);
        });
    }

    protected void ProcessNextWithDelay(DialogueGraphData dialogue, NodeData nodeData, float secondsDelay)
    {
        StartCoroutine(ProcessNextWithDelayRoutine(dialogue, nodeData, secondsDelay));
    }

    private IEnumerator ProcessNextWithDelayRoutine(DialogueGraphData dialogue, NodeData nodeData, float secondsDelay)
    {
        yield return new WaitForSeconds(secondsDelay);

        ProcessNext(dialogue, nodeData);
    }
}
