using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;


public class DialogueGraphEditorView : GraphView
{
    public const string GRAPH_STYLE_FOLDER = "GraphStyle";
    private const string GRAPH_STYLE_FILE = "DialogueGraph";
    private const float PASTE_OFFSET_Y = 20.0f;
    private const string COMPATIBLE_CLIPBOARD_DATA_INDICATOR = "GRAPH_COPY_DATA";
    private readonly Vector2 DEFAULT_COMMENT_BLOCK_SIZE = new Vector2(300, 200);


    private Blackboard blackboard = new Blackboard();
    private DialogueGraphWindow editorWindow;
    private NodeCreationWindow nodeCreationWindow;
    private EntryPointNodeEditorView entryNode;


    public DialogueGraphEditorView(DialogueGraphWindow editorWindow)
    {
        this.editorWindow = editorWindow;

        styleSheets.Add(Resources.Load<StyleSheet>(Path.Combine(GRAPH_STYLE_FOLDER, GRAPH_STYLE_FILE)));
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        this.AddManipulator(new FreehandSelector());

        SetGrid();

        entryNode = new EntryPointNodeEditorView();
        AddElement(entryNode);

        AddNodeCreationWindow(editorWindow);

        this.serializeGraphElements += Copy;
        this.unserializeAndPaste += PasteOrCut;
        this.canPasteSerializedData += CanCopyPaste;
    }


    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();
        var startPortView = startPort;

        ports.ForEach((port) =>
        {
            var portView = port;
            if (startPortView != portView && startPortView.node != portView.node)
            {
                compatiblePorts.Add(port);
            }
        });

        return compatiblePorts;
    }

    public Group AddCommentBlock(CommentBlockData commentBlockData, Vector2 position)
    {
        var group = new Group
        {
            autoUpdateGeometry = true,
            title = commentBlockData.title
        };
        AddElement(group);
        group.SetPosition(new Rect(position, DEFAULT_COMMENT_BLOCK_SIZE));
        return group;
    }
    public void AddVariableToBlackBoard(ExposedVariable variable)
    {
        while (ExposedVariables.Any(x => x.Name == variable.Name))
        {
            variable.Name = $"{variable.Name}(1)";
        }

        ExposedVariables.Add(variable);

        var container = new VisualElement();
        var field = new BlackboardField
        {
            text = variable.Name,
            typeText = "Variable"
        };
        container.Add(field);

        var propertyValueTextField = new TextField("Value:");
        propertyValueTextField.value = variable.Value;
        propertyValueTextField.RegisterValueChangedCallback(evt =>
        {
            var index = ExposedVariables.FindIndex(x => x.Name == variable.Name);
            ExposedVariables[index].Value = evt.newValue;
        });
        var blackboardRow = new BlackboardRow(field, propertyValueTextField);
        container.Add(blackboardRow);

        blackboard.Add(container);
    }
    public new void Clear()
    {
        foreach (var perNode in Nodes)
        {
            Edges.Where(x => x.input.node == perNode).ToList()
                .ForEach(edge => RemoveElement(edge));

            RemoveElement(perNode);
        }
    }
    public void AddNodes(List<NodeData> nodeDatas)
    {
        foreach (var nodeData in nodeDatas)
        {
            AddNode(nodeData);
        }
    }
    public void AddNode(NodeData nodeData)
    {
        AddElement(NodeDataToEditorNode(nodeData));
    }
    public void ConnectNodes(List<NodeLinkData> nodeLinks)
    {
        for (int i = 0; i < Nodes.Count; i++)
        {
            NodeEditorView node = Nodes[i];
            List<NodeLinkData> nodeConnections = nodeLinks.Where(x => x.sourceNodeGUID == node.GUID).ToList();

            var outputElements = node.outputContainer.Children();

            foreach (var outputElement in outputElements)
            {
                Port outputPort = outputElement.Q<Port>();
                var nodeLinkDatas = nodeConnections.Where(x => x.sourcePortName == outputPort.portName);

                foreach (var nodeLinkData in nodeLinkDatas)
                {
                    var targetNodeGUID = nodeLinkData.destinationNodeGUID;
                    var targetNode = Nodes.FirstOrDefault(x => x.GUID == targetNodeGUID);
                    if (targetNode != null)
                    {
                        Port inputPort = (Port)targetNode.inputContainer[0];

                        LinkNodesTogether(outputPort, inputPort);
                    }
                }
            }
        }
    }
    public void ConnectNodes()
    {

    }
    public void AddExposedProperties(List<ExposedVariable> exposedProperties)
    {
        ExposedVariables.Clear();
        blackboard.Clear();
        foreach (var exposedProperty in exposedProperties)
        {
            AddVariableToBlackBoard(exposedProperty);
        }
    }
    public void GenerateCommentBlocks(List<CommentBlockData> commentBlockDatas)
    {
        foreach (var commentBlock in CommentBlockGroups)
        {
            RemoveElement(commentBlock);
        }

        foreach (var commentBlockData in commentBlockDatas)
        {
            var block = AddCommentBlock(commentBlockData, commentBlockData.position);
            block.AddElements(Nodes.Where(x => commentBlockData.nodeGuids.Contains(x.GUID)));
        }
    }

    private void LinkNodesTogether(Port outputPort, Port inputPort)
    {
        var tempEdge = new Edge()
        {
            output = outputPort,
            input = inputPort
        };
        tempEdge?.input.Connect(tempEdge);
        tempEdge?.output.Connect(tempEdge);
        Add(tempEdge);
    }
    private void AddNodeCreationWindow(DialogueGraphWindow editorWindow)
    {
        nodeCreationWindow = ScriptableObject.CreateInstance<NodeCreationWindow>();
        nodeCreationWindow.Init(editorWindow, this);
        nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), nodeCreationWindow);
    }
    private void SetGrid()
    {
        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();
    }
    private Node NodeDataToEditorNode(NodeData nodeData)
    {
        if (nodeData is EntryNode)
        {
            return new EntryPointNodeEditorView(nodeData as EntryNode);
        }
        else if (nodeData is DialogueNode)
        {
            return new DialogueNodeEditorView(nodeData as DialogueNode);
        }
        else if (nodeData is CharacterNode)
        {
            return new ShowCharacterNodeEditorView(nodeData as CharacterNode);
        }
        else if (nodeData is MonologueNode)
        {
            return new StartMonologueEditorNodeView(nodeData as MonologueNode);
        }
        else if (nodeData is SplitMessageNode)
        {
            return new StartSplitMessageEditorNodeView(nodeData as SplitMessageNode);
        }
        else if (nodeData is StopNode)
        {
            return new StopNodeEditorView(nodeData as StopNode);
        }
        else if (nodeData is RandomOutputNode)
        {
            return new RandomOutputNodeEditorView(nodeData as RandomOutputNode);
        }
        else if (nodeData is LocationNode)
        {
            return new LocationNodeEditorView(nodeData as LocationNode);
        }
        else
        {
            throw new Exception("There is no Node Editor View type you want to load!");
        }
    }
    private string Copy(IEnumerable<GraphElement> elements)
    {
        string stringData = COMPATIBLE_CLIPBOARD_DATA_INDICATOR + '\n';
        foreach (var element in elements)
        {
            if (element is NodeEditorView)
            {
                NodeEditorView nodeView = element as NodeEditorView;
                var nodeData = nodeView.Data;
                stringData += nodeData.GetType().AssemblyQualifiedName.ToString() + '\n';
                stringData += JsonUtility.ToJson(nodeData) + '\n';
            }
            else if (element is Edge)
            {
                Edge edge = element as Edge;

                var outputPort = edge.output;
                var inputPort = edge.input;
                var outputNode = (outputPort.node as NodeEditorView);
                var inputNode = (edge.input.node as NodeEditorView);

                var nodeLinkData = new NodeLinkData
                {
                    sourceNodeGUID = outputNode.GUID,
                    sourcePortName = outputPort.portName,
                    destinationNodeGUID = inputNode.GUID,
                };

                stringData += nodeLinkData.GetType().AssemblyQualifiedName.ToString() + '\n';
                stringData += JsonUtility.ToJson(nodeLinkData) + '\n';
            }
        }

        // return data as string (to clipboard)
        return stringData;
    }
    private void PasteOrCut(string operationName, string data)
    {
        data = data.Substring(COMPATIBLE_CLIPBOARD_DATA_INDICATOR.Length);

        List<NodeData> nodeDatas = new List<NodeData>();
        List<NodeLinkData> nodeLinkDatas = new List<NodeLinkData>();

        string[] dataArray = data.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        if (operationName == "Paste" || operationName == "Cut")
        {
            for (int i = 0; i < dataArray.Length - 1; i += 2)
            {
                Type type = Type.GetType(dataArray[i]);
                object element = JsonUtility.FromJson(dataArray[i + 1], type);
                if (typeof(NodeData).IsAssignableFrom(type))
                {
                    NodeData nodeData = element as NodeData;
                    nodeDatas.Add(nodeData);
                }
                else if (typeof(NodeLinkData).IsAssignableFrom(type))
                {
                    var nodeLinkData = element as NodeLinkData;
                    nodeLinkDatas.Add(nodeLinkData);
                }
            }
        }
        switch (operationName)
        {
            case "Paste":
                {
                    Paste(nodeDatas, nodeLinkDatas);

                    break;
                }
            case "Cut":
                {
                    Cut(nodeDatas, nodeLinkDatas);

                    break;
                }
        }
    }
    private void Paste(List<NodeData> nodeDatas, List<NodeLinkData> nodeLinkDatas)
    {
        for (int i = 0; i < nodeDatas.Count; i++)
        {
            NodeData nodeData = nodeDatas[i];
            string oldGuid = nodeData.GUID;
            string newGuid = Guid.NewGuid().ToString();
            nodeLinkDatas.Where(x => x.destinationNodeGUID == oldGuid).ToList().ForEach(x => x.destinationNodeGUID = newGuid);
            nodeLinkDatas.Where(x => x.sourceNodeGUID == oldGuid).ToList().ForEach(x => x.sourceNodeGUID = newGuid);
            nodeData.GUID = newGuid;
            nodeData.position += new Vector2(0, PASTE_OFFSET_Y);
            AddNode(nodeData);
        }
        for (int i = 0; i < nodeLinkDatas.Count; i++)
        {
            var nodeLinkData = nodeLinkDatas[i];

            var destNode = nodeDatas.FirstOrDefault(x => x.GUID == nodeLinkData.destinationNodeGUID);
            var sourceNode = nodeDatas.FirstOrDefault(x => x.GUID == nodeLinkData.sourceNodeGUID);
            if (destNode == null || sourceNode == null)
            {
                nodeLinkDatas.RemoveAt(i);
                i--;
            }
        }
        ConnectNodes(nodeLinkDatas);
    }
    private void Cut(List<NodeData> nodeDatas, List<NodeLinkData> nodeLinkDatas)
    {
        // Remove prev nodes (not necessary)
        foreach (var nodeData in nodeDatas)
        {
            var node = Nodes.FirstOrDefault(x => x.GUID == nodeData.GUID);
            if (node != null)
                Remove(node);
        }
        Paste(nodeDatas, nodeLinkDatas);
    }
    private bool CanCopyPaste(string data)
    {
        return data.StartsWith(COMPATIBLE_CLIPBOARD_DATA_INDICATOR);
    }


    public EntryPointNodeEditorView EntryNode => entryNode;
    public List<NodeEditorView> Nodes => nodes.ToList().Cast<NodeEditorView>().ToList();
    public List<NodeData> NodeDatas
    {
        get
        {
            List<NodeData> nodeDatas = new List<NodeData>();

            foreach (var node in Nodes)
            {
                if (node != null)
                {
                    nodeDatas.Add(node.Data);
                }
            }

            return nodeDatas;
        }
    }
    public List<Edge> Edges => edges.ToList();
    public List<NodeLinkData> NodeLinkDatas
    {
        get
        {
            List<NodeLinkData> nodeLinkDatas = new List<NodeLinkData>();

            var connectedSockets = Edges.Where(x => x.input.node != null && x.output.node != null).ToArray();
            for (var i = 0; i < connectedSockets.Count(); i++)
            {
                var outputPort = connectedSockets[i].output;
                var inputPort = connectedSockets[i].input;
                var outputNode = (outputPort.node as NodeEditorView);
                var inputNode = (connectedSockets[i].input.node as NodeEditorView);

                var nodeLinkData = new NodeLinkData
                {
                    sourceNodeGUID = outputNode.GUID,
                    sourcePortName = outputPort.portName,
                    destinationNodeGUID = inputNode.GUID,
                };

                nodeLinkDatas.Add(nodeLinkData);
            }

            return nodeLinkDatas;
        }
    }
    private List<Group> CommentBlockGroups => graphElements.ToList().Where(x => x is Group).Cast<Group>().ToList();
    public List<CommentBlockData> CommentBlockDatas
    {
        get
        {
            List<CommentBlockData> commentBlockDatas = new List<CommentBlockData>();

            foreach (var block in CommentBlockGroups)
            {
                var nodes = block.containedElements.Where(x => x is NodeEditorView).Cast<NodeEditorView>().Select(x => x.GUID).ToList();

                commentBlockDatas.Add(new CommentBlockData
                {
                    nodeGuids = nodes,
                    title = block.title,
                    position = block.GetPosition().position
                });
            }

            return commentBlockDatas;
        }
    }
    public List<ExposedVariable> ExposedVariables { get; private set; } = new List<ExposedVariable>();
    public Blackboard Blackboard { get => blackboard; set => blackboard = value; }
}
