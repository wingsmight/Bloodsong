using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class NodeEditorView : Node
{
    private const string NODE_STYLE_FILE = "Node";
    protected static Vector2 DEFAULT_POSTION = new Vector2(100, 200);
    protected static Vector2 DEFAULT_SIZE = new Vector2(100, 200);


    protected string guid;


    public NodeEditorView()
    {
        guid = Guid.NewGuid().ToString();
        styleSheets.Add(Resources.Load<StyleSheet>(DialogueGraphEditorView.GRAPH_STYLE_FOLDER + "/" + NODE_STYLE_FILE));


        RefreshExpandedState();
        SetPosition(new Rect(DEFAULT_POSTION, DEFAULT_SIZE));
    }
    public NodeEditorView(NodeData data) : this()
    {
        guid = data.GUID;

        SetPosition(new Rect(data.Position, DEFAULT_SIZE));
    }


    public Port AddPort(Direction direction, Port.Capacity capacity)
    {
        var port = InstantiatePort(Orientation.Horizontal, direction, capacity, typeof(float));
        port.portName = direction.ToString();
        if (direction == Direction.Input)
        {
            inputContainer.Add(port);
        }
        else
        {
            outputContainer.Add(port);
        }

        RefreshPorts();

        return port;
    }


    public abstract NodeData Data { get; }

    public string GUID { get => guid; set => guid = value; }
    public Vector2 Position
    {
        get
        {
            return GetPosition().position;
        }
        set
        {
            SetPosition(new Rect(value, DEFAULT_SIZE));
        }
    }
}
