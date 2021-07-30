using System;
using System.Collections.Generic;
using UnityEngine;

public class LocationNodeView : NodeView<LocationNode>
{
    [SerializeField] private BackgroundView backgroundView;


    private DialogueGraphData dialogue;
    private LocationNode nodeData;


    public override void Act(DialogueGraphData dialogue, LocationNode nodeData)
    {
        this.dialogue = dialogue;
        this.nodeData = nodeData;

        Action processNextOnShown = null;
        processNextOnShown = () =>
        {
            backgroundView.OnShown -= processNextOnShown;

            ProcessNext(dialogue, nodeData);
        };
        backgroundView.OnShown += processNextOnShown;

        backgroundView.Show(new Location(nodeData.name));
    }
}
