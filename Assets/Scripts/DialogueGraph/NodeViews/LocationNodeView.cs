using System;
using System.Collections.Generic;
using UnityEngine;

public class LocationNodeView : NodeView<LocationNode>
{
    [SerializeField] private BackgroundView backgroundView;


    public override void Act(DialogueGraphData dialogue, LocationNode nodeData)
    {
        Action processNextOnShown = null;
        processNextOnShown = () =>
        {
            backgroundView.OnShown -= processNextOnShown;

            ProcessNext(dialogue, nodeData);
        };
        backgroundView.OnShown += processNextOnShown;

        backgroundView.Show(new Location(nodeData.name));
    }
    public void ActWithoutProcessNext(DialogueGraphData dialogue, LocationNode nodeData)
    {
        backgroundView.Show(new Location(nodeData.name));
    }
}
