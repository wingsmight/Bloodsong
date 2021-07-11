using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EntryPointNodeEditorView : NodeEditorView
{
    public EntryPointNodeEditorView() : base()
    {
        title = "START";

        AddPort(Direction.Output, Port.Capacity.Single);

        capabilities &= ~Capabilities.Movable;
        capabilities &= ~Capabilities.Deletable;
    }
    public EntryPointNodeEditorView(EntryNode data) : this()
    {
        guid = data.GUID;
        Position = data.Position;
    }

    public override NodeData Data => new EntryNode(guid, Position);
}