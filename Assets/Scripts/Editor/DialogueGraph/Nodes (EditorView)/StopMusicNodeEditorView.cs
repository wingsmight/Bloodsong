using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class StopMusicNodeEditorView : NodeEditorView
{
    public StopMusicNodeEditorView() : base()
    {
        title = "Stop music";

        AddPort(Direction.Input, Port.Capacity.Multi);
        AddPort(Direction.Output, Port.Capacity.Single);
    }
    public StopMusicNodeEditorView(StopMusicNode data) : this()
    {
        guid = data.GUID;
        Position = data.Position;
    }


    public override NodeData Data =>
        new StopMusicNode(guid, Position);
}
