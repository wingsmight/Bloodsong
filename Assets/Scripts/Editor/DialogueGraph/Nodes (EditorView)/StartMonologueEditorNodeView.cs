using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class StartMonologueEditorNodeView : NodeEditorView
{
    private TextField textField;

    private string text;


    public StartMonologueEditorNodeView() : base()
    {
        title = "Start monologue";

        AddPort(Direction.Input, Port.Capacity.Multi);
        AddPort(Direction.Output, Port.Capacity.Single);

        textField = new TextField("");
        textField.RegisterValueChangedCallback(evt =>
        {
            string wrappedText = TextFieldWrapper.Wrap(evt.newValue, 75);
            string unwrappedText = TextFieldWrapper.Unwrap(wrappedText);

            text = unwrappedText;

            textField.SetValueWithoutNotify(wrappedText);
        });
        textField.multiline = true;
        mainContainer.Add(textField);
    }
    public StartMonologueEditorNodeView(MonologueNode data) : this()
    {
        guid = data.GUID;
        Position = data.Position;

        text = data.text;

        textField.SetValueWithoutNotify(TextFieldWrapper.Wrap(text, 75));
    }


    public override NodeData Data => new MonologueNode(guid, Position, text);

    public string Text => text;
}
