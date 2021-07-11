using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class EnterCharacterNodeEditorView : NodeEditorView
{
    private EnteredVisiter enteredVisiter;

    private string visiterName;


    public EnterCharacterNodeEditorView() : base()
    {
        title = "Enter visiter";

        AddPort(Direction.Input, Port.Capacity.Multi);
        AddPort(Direction.Output, Port.Capacity.Single);

        enteredVisiter = ScriptableObject.CreateInstance<EnteredVisiter>();
        if (!string.IsNullOrEmpty(visiterName))
        {
            enteredVisiter.visiter = ScriptableObjectFinder.Get<Character>(visiterName);
        }
        SerializedObject serializedObject = new SerializedObject(enteredVisiter);
        SerializedProperty property = serializedObject.FindProperty("visiter");
        PropertyField prop = new PropertyField(property, "Visister:");

        prop.RegisterCallback<ChangeEvent<UnityEngine.Object>>(evt =>
        {
            visiterName = evt?.newValue?.name;
        });
        prop.BindProperty(property);
        mainContainer.Add(prop);

        serializedObject.ApplyModifiedProperties();
    }
    public EnterCharacterNodeEditorView(EnterCharacterNode data) : this()
    {
        guid = data.GUID;
        Position = data.Position;

        visiterName = data.visiterName;

        if (!string.IsNullOrEmpty(visiterName))
        {
            enteredVisiter.visiter = ScriptableObjectFinder.Get<Character>(visiterName);
        }
    }


    public override NodeData Data => new EnterCharacterNode(guid, Position, visiterName);

    public string VisiterName => visiterName;
}

public class EnteredVisiter : ScriptableObject
{
    public Character visiter;
}
