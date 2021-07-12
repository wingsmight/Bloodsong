using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class StopNodeEditorView : NodeEditorView
{
    public StopNodeEditorView() : base()
    {
        title = "Stop";

        AddPort(Direction.Input, Port.Capacity.Multi);
    }
    public StopNodeEditorView(StopNode data) : this()
    {
        guid = data.GUID;
        Position = data.Position;
    }


    public override NodeData Data => new StopNode(guid, Position);
}