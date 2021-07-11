using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class StartSplitMessageEditorNodeView : NodeEditorView
{
    private const int DIALOGUE_TEXT_FIELD_LENGTH = 75;


    private List<TextField> textFields;

    private List<string> texts = new List<string>();


    public StartSplitMessageEditorNodeView() : base()
    {
        title = "Start split message";

        AddPort(Direction.Input, Port.Capacity.Multi);
        AddPort(Direction.Output, Port.Capacity.Single);

        textFields = new List<TextField>();

        #region addTextButton
        var addTextButton = new Button(() =>
        {
            AddTextField("");
        });
        addTextButton.text = "Add Text";

        titleButtonContainer.Add(addTextButton);
        #endregion addResponseButton
    }
    public StartSplitMessageEditorNodeView(SplitMessageNode data) : this()
    {
        guid = data.GUID;
        Position = data.Position;

        foreach (var text in data.text)
        {
            AddTextField(text);
        }
    }

    public void AddTextField(string text)
    {
        int contentIndex = texts.Count;
        texts.Add(text);

        TextField newTextField = new TextField();
        newTextField.RegisterValueChangedCallback(evt =>
        {
            SetText(evt.newValue);
        });
        newTextField.value = texts[contentIndex];
        SetText(text);
        newTextField.multiline = true;
        textFields.Add(newTextField);

        mainContainer.Add(newTextField);

        void SetText(string newText)
        {
            string wrappedText = TextFieldWrapper.Wrap(newText, DIALOGUE_TEXT_FIELD_LENGTH);
            string unwrappedText = TextFieldWrapper.Unwrap(wrappedText);

            texts[contentIndex] = unwrappedText;

            newTextField.SetValueWithoutNotify(wrappedText);
        }
    }


    public override NodeData Data => new SplitMessageNode(guid, Position, texts);

    public List<string> Texts => texts;
}
