using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationNodeView : NodeView<LocationNode>
{
    [SerializeField] private BackgroundView backgroundView;


    public override void Act(DialogueGraphData dialogue, LocationNode nodeData)
    {
        backgroundView.Show(new Location(nodeData.name));

        ProcessNext(dialogue, nodeData);
    }
}
