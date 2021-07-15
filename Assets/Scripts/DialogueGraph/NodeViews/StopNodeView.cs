using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StopNodeView : NodeView<StopNode>
{
    public event UnityAction OnGraphEnded;


    public void Act()
    {
        OnGraphEnded?.Invoke();
    }

    public override void Act(DialogueGraphData dialogue, StopNode nodeData)
    {
        Act();
    }
}

