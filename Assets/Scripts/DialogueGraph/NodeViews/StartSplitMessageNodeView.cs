using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSplitMessageNodeView : NodeView<SplitMessageNode>
{
    private const float DELAY_AFTER_STOP = 0.5f;

    [SerializeField] private SplitMessagePanel splitTextAppearing;


    public override void Act(DialogueGraphData dialogue, SplitMessageNode nodeData)
    {
        splitTextAppearing.Show(nodeData.text);
        splitTextAppearing.AddActionAfterHide(() =>
        {
            ProcessNextWithDelay(dialogue, nodeData, DELAY_AFTER_STOP);
        });
    }
    public override void Stop()
    {
        splitTextAppearing.Reset();
        splitTextAppearing.Hide();
    }
}
