using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class StartMonologueEditorNodeView : NodeEditorView
{
    private const int TEXT_FIELD_LENGTH = 75;


    private SerializedProperty characterProperty;
    private List<TextField> phraseTextFields = new List<TextField>();


    public StartMonologueEditorNodeView() : base()
    {
        title = "Start monologue";

        AddPort(Direction.Input, Port.Capacity.Multi);
        AddPort(Direction.Output, Port.Capacity.Single);

        #region speakerField
        CharacterSO characterSO = ScriptableObject.CreateInstance<CharacterSO>();
        SerializedObject serializedObject = new SerializedObject(characterSO);
        characterProperty = serializedObject.FindProperty("character");
        PropertyField characterField = new PropertyField(characterProperty, "Character:");
        characterField.BindProperty(characterProperty);
        mainContainer.Add(characterField);

        serializedObject.ApplyModifiedProperties();
        #endregion

        #region addPhraseButton
        var addPhraseButton = new Button(() =>
        {
            AddPhrase("");
        });
        addPhraseButton.text = "Add phrase";

        contentContainer.Add(addPhraseButton);
        #endregion addPhraseButton
    }
    public StartMonologueEditorNodeView(MonologueNode data) : this()
    {
        guid = data.GUID;
        Position = data.Position;

        SetSpeaker(data.speakerName);
        foreach (var text in data.texts)
        {
            AddPhrase(text);
        }
    }
    public void AddPhrase(string text)
    {
        TextField newTextField = new TextField();
        newTextField.RegisterValueChangedCallback(evt =>
        {
            SetText(evt.newValue);
        });
        newTextField.value = text;
        SetText(text);
        newTextField.multiline = true;
        phraseTextFields.Add(newTextField);

        mainContainer.Add(newTextField);

        void SetText(string newText)
        {
            string wrappedText = TextFieldWrapper.Wrap(newText, TEXT_FIELD_LENGTH);

            newTextField.SetValueWithoutNotify(wrappedText);
        }
    }

    private void SetSpeaker(string speakerName)
    {
        if (!string.IsNullOrEmpty(speakerName))
        {
            characterProperty.SetValue<Character>(ScriptableObjectFinder.Get(speakerName, typeof(Character)) as Character);
        }
    }


    public override NodeData Data =>
        new MonologueNode(guid, Position,
            phraseTextFields.Select(x => TextFieldWrapper.Unwrap(x.value)).ToList(),
            characterProperty.GetValue<Character>());
}

public class CharacterSO : ScriptableObject
{
    public Character character;
}
