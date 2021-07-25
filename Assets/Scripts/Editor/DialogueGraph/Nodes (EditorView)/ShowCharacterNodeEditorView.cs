using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class ShowCharacterNodeEditorView : NodeEditorView
{
    private SerializedProperty characterProperty;
    private EnumField positionEnumField;
    private EnumField emotionEnumField;
    private EnumField directionEnumField;


    public ShowCharacterNodeEditorView() : base()
    {
        title = "Show character";

        AddPort(Direction.Input, Port.Capacity.Multi);
        AddPort(Direction.Output, Port.Capacity.Single);

        #region characterField
        CharacterSO characterSO = ScriptableObject.CreateInstance<CharacterSO>();
        SerializedObject serializedObject = new SerializedObject(characterSO);
        characterProperty = serializedObject.FindProperty("character");
        PropertyField characterField = new PropertyField(characterProperty, "Character:");
        characterField.BindProperty(characterProperty);
        mainContainer.Add(characterField);

        serializedObject.ApplyModifiedProperties();
        #endregion

        #region positionEnum
        CharacterView.Position initPosition = (CharacterView.Position)0;
        positionEnumField = new EnumField(initPosition);
        positionEnumField.value = initPosition;
        positionEnumField.label = "Position:";

        mainContainer.Add(positionEnumField);
        #endregion

        #region emotionEnum
        Character.Emotion initEmotion = (Character.Emotion)0;
        emotionEnumField = new EnumField(initEmotion);
        emotionEnumField.value = initEmotion;
        emotionEnumField.label = "Emotion:";

        mainContainer.Add(emotionEnumField);
        #endregion

        #region directionEnum
        CharacterView.Direction initDirection = (CharacterView.Direction)0;
        directionEnumField = new EnumField(initDirection);
        directionEnumField.value = initDirection;
        directionEnumField.label = "Direction:";

        mainContainer.Add(directionEnumField);
        #endregion
    }
    public ShowCharacterNodeEditorView(CharacterNode data) : this()
    {
        guid = data.GUID;
        Position = data.Position;

        SetCharacter(data.character == null ? "" : data.character.name);
        SetCharacterPosition(data.character.position);
        SetCharacterEmotion(data.character.emotion);
        SetDirection(data.direction);
    }

    private void SetCharacter(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            characterProperty.SetValue<Character>(ScriptableObjectFinder.Get(name, typeof(Character)) as Character);
        }
    }
    private void SetCharacterPosition(CharacterView.Position position)
    {
        positionEnumField.value = position;
    }
    private void SetCharacterEmotion(Character.Emotion emotion)
    {
        emotionEnumField.value = emotion;
    }
    private void SetDirection(CharacterView.Direction direction)
    {
        directionEnumField.value = direction;
    }


    public override NodeData Data =>
        new CharacterNode(guid, Position,
            new CharacterProperty(
                characterProperty.GetObjectReferenceValueName(),
                (CharacterView.Position)positionEnumField.value,
                (Character.Emotion)emotionEnumField.value),
            (CharacterView.Direction)directionEnumField.value);
}
