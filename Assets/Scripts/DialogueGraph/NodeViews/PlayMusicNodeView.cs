using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicNodeView : NodeView<MusicNode>
{
    [SerializeField] private float smoothDurationSeconds;


    public override void Act(DialogueGraphData dialogue, MusicNode nodeData)
    {
        AudioPlayer.Instance.PlaySmoothy(AudioPlayer.GetAudioFromResource(nodeData.name), smoothDurationSeconds);

        ProcessNext(dialogue, nodeData);
    }
    public override void Stop()
    {
        base.Stop();

        AudioPlayer.Instance.Stop();
    }
}
