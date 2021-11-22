using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusicNodeView : NodeView<StopMusicNode>
{
    [SerializeField] private float smoothDurationSeconds;


    public override void Act(DialogueGraphData dialogue, StopMusicNode nodeData)
    {
        AudioPlayer.Instance.StopSmoothy(smoothDurationSeconds, () =>
        {
            ProcessNext(dialogue, nodeData);
        });
    }
    public override void Stop()
    {
        base.Stop();

        throw new NotImplementedException();
    }
}
