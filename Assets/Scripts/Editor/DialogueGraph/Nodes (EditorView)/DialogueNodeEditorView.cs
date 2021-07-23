using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueNodeEditorView : NodeEditorView
{
    private const int TEXT_FIELD_LENGTH = 75;


    private SerializedProperty characterProperty;
    private TextField dialogueTextField;
    private List<DialogueResponseData> responses = new List<DialogueResponseData>();


    public DialogueNodeEditorView() : base()
    {
        title = "Dialogue node";

        AddPort(Direction.Input, Port.Capacity.Multi);

        #region speakerField
        CharacterSO characterSO = ScriptableObject.CreateInstance<CharacterSO>();
        SerializedObject serializedObject = new SerializedObject(characterSO);
        characterProperty = serializedObject.FindProperty("character");
        PropertyField characterField = new PropertyField(characterProperty, "Character:");
        characterField.BindProperty(characterProperty);
        mainContainer.Add(characterField);

        serializedObject.ApplyModifiedProperties();
        #endregion

        #region dialogueTextField
        dialogueTextField = new TextField("");
        dialogueTextField.RegisterValueChangedCallback(evt =>
        {
            SetText(evt.newValue);
        });
        dialogueTextField.multiline = true;

        mainContainer.Add(dialogueTextField);
        #endregion dialogueTextField

        #region addResponseButton
        var addResponseButton = new Button(() =>
        {
            var mousePosition = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
            mousePosition = new Vector2(mousePosition.x, mousePosition.y + 35);

            var portChoiceWindow = ScriptableObject.CreateInstance<DialoguePortChoiceMenu>();
            portChoiceWindow.Init(this);
            SearchWindow.Open(new SearchWindowContext(mousePosition), portChoiceWindow);
        });
        addResponseButton.text = "Add response";

        titleButtonContainer.Add(addResponseButton);
        #endregion addResponseButton
    }
    public DialogueNodeEditorView(DialogueNode data) : this()
    {
        guid = data.GUID;
        Position = data.Position;

        SetSpeaker(data.speakerName);
        SetText(data.dialogueText);
        for (int i = 0; i < data.responses.Count; i++)
        {
            AddResponsePort(data.responses[i]);
        }
    }

    public void AddResponsePort(DialogueResponseData responseData)
    {
        DialoguePort generatedPort;
        if (responseData is DialogueStatResponseData)
        {
            generatedPort = new DialogueStatPort(responseData as DialogueStatResponseData);
        }
        else if (responseData is DialogueStatsResponseData)
        {
            generatedPort = new DialogueStatsPort(responseData as DialogueStatsResponseData);
        }
        else
        {
            generatedPort = new DialoguePort(responseData);
        }

        var deleteButton = new Button(() =>
        {
            RemovePort(generatedPort);
            responses.Remove(responseData);
        });
        deleteButton.text = "X";

        generatedPort.contentContainer.Add(deleteButton);

        outputContainer.Add(generatedPort);

        RefreshPorts();
        RefreshExpandedState();

        responses.Add(responseData);
    }
    private void RemovePort(Port socket)
    {
        outputContainer.Remove(socket);
        RefreshPorts();
        RefreshExpandedState();
    }
    private void SetText(string text)
    {
        string wrappedText = TextFieldWrapper.Wrap(text, TEXT_FIELD_LENGTH);

        dialogueTextField.SetValueWithoutNotify(wrappedText);
    }
    private void SetSpeaker(string speakerName)
    {
        if (!string.IsNullOrEmpty(speakerName))
        {
            characterProperty.SetValue<Character>(ScriptableObjectFinder.Get(speakerName, typeof(Character)) as Character);
        }
    }


    public override NodeData Data
    {
        get
        {
            //Remove unattached ports
            var outPorts = outputContainer.Children().ToList();
            for (int i = 0; i < outPorts.Count && i < responses.Count; i++)
            {
                if (!(outPorts[i] as Port).connected)
                {
                    outPorts.RemoveAt(i);
                    outputContainer.RemoveAt(i);
                    responses.RemoveAt(i);

                    i--;
                }
            }

            return new DialogueNode(guid, Position, TextFieldWrapper.Unwrap(dialogueTextField.value), responses,
            characterProperty.GetObjectReferenceValueName());
        }
    }

    public List<DialogueResponseData> Responses => responses;
}