using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSplitMessageNodeView : NodeView<SplitMessageNode>
{
    private const float DELAY_AFTER_STOP = 0.5f;

    [SerializeField] private SplitTextAppearing splitTextAppearing;


    public override void Act(DialogueGraphData dialogue, SplitMessageNode nodeData)
    {
        splitTextAppearing.Type(nodeData.text);
        splitTextAppearing.OnStopTyping += () =>
        {
            Debug.Log("AAAAA");

            ProcessNextWithDelay(dialogue, nodeData, DELAY_AFTER_STOP);
        };
    }
}
