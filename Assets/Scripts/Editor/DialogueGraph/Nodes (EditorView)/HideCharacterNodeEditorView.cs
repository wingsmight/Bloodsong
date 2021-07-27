using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class HideCharacterNodeEditorView : NodeEditorView
{
    private EnumField positionEnumField;


    public HideCharacterNodeEditorView() : base()
    {
        title = "Hide character";

        AddPort(Direction.Input, Port.Capacity.Multi);
        AddPort(Direction.Output, Port.Capacity.Single);

        #region positionEnum
        CharacterView.Position initPosition = (CharacterView.Position)0;
        positionEnumField = new EnumField(initPosition);
        positionEnumField.value = initPosition;
        positionEnumField.label = "Position:";

        mainContainer.Add(positionEnumField);
        #endregion
    }
    public HideCharacterNodeEditorView(CharacterPositionNode data) : this()
    {
        guid = data.GUID;
        Position = data.Position;

        SetCharacterPosition(data.characterPosition);
    }


    private void SetCharacterPosition(CharacterView.Position position)
    {
        positionEnumField.value = position;
    }


    public override NodeData Data =>
        new CharacterPositionNode(guid, Position, (CharacterView.Position)positionEnumField.value);
}
