using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueNodeEditorView : NodeEditorView
{
    private const int DIALOGUE_TEXT_FIELD_LENGTH = 75;

    private TextField dialogueTextField;

    private string dialogueText;
    private List<DialogueResponseData> responses = new List<DialogueResponseData>();


    public DialogueNodeEditorView() : base()
    {
        title = "Dialogue node";

        AddPort(Direction.Input, Port.Capacity.Multi);

        #region dialogueTextField
        dialogueTextField = new TextField("");
        dialogueTextField.RegisterValueChangedCallback(evt =>
        {
            string wrappedText = TextFieldWrapper.Wrap(evt.newValue, DIALOGUE_TEXT_FIELD_LENGTH);
            string unwrappedText = TextFieldWrapper.Unwrap(wrappedText);

            dialogueText = unwrappedText;

            dialogueTextField.SetValueWithoutNotify(wrappedText);
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

        dialogueText = data.dialogueText;
        dialogueTextField.SetValueWithoutNotify(TextFieldWrapper.Wrap(dialogueText, DIALOGUE_TEXT_FIELD_LENGTH));

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

            return new DialogueNode(guid, Position, dialogueText, responses);
        }
    }

    public string DialogueText => dialogueText;
    public List<DialogueResponseData> Responses => responses;
}