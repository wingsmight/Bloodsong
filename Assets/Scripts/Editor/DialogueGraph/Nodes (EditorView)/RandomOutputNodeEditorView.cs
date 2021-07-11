using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor;
using System;

public class RandomOutputNodeEditorView : NodeEditorView
{
    //private List<Port> outports = new List<Port>();


    public RandomOutputNodeEditorView()
    {
        title = "Random between outports";

        AddPort(Direction.Input, Port.Capacity.Multi);
        AddPort(Direction.Output, Port.Capacity.Multi);
        //AddOutport();

        //var addPortButton = new Button(() =>
        //{
        //    AddOutport();
        //});
        //addPortButton.text = "Add outport";

        //titleButtonContainer.Add(addPortButton);

        //var removePortButton = new Button(() =>
        //{
        //    RemoveOutport();
        //});
        //removePortButton.text = "Remove outport";

        //titleButtonContainer.Add(removePortButton);
    }
    public RandomOutputNodeEditorView(RandomOutputNode data) : this()
    {
        guid = data.GUID;
        Position = data.Position;
    }

    //public void AddOutport()
    //{
    //    var newPort = AddPort(Direction.Output, Port.Capacity.Single);
    //    newPort.portName = "Output" + outports.Count;
    //    outports.Add(newPort);

    //    outputContainer.Add(newPort);

    //    RefreshPorts();
    //    RefreshExpandedState();
    //}
    //public void RemoveOutport()
    //{
    //    if(outports.Count > 1)
    //    {
    //        outputContainer.Remove(outports[outports.Count - 1]);
    //        outports.RemoveAt(outports.Count - 1);

    //        RefreshPorts();
    //        RefreshExpandedState();
    //    }
    //}

    public override NodeData Data => new RandomOutputNode(guid, Position);
}
