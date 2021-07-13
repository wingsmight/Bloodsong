using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class LocationNodeEditorView : NodeEditorView
{
    private SerializedProperty spriteProperty;


    public LocationNodeEditorView() : base()
    {
        title = "Show location";

        AddPort(Direction.Input, Port.Capacity.Multi);
        AddPort(Direction.Output, Port.Capacity.Single);

        #region spriteField
        SpriteSO spriteSO = ScriptableObject.CreateInstance<SpriteSO>();
        SerializedObject serializedObject = new SerializedObject(spriteSO);
        spriteProperty = serializedObject.FindProperty("sprite");
        PropertyField spriteField = new PropertyField(spriteProperty, "Sprite:");
        spriteField.BindProperty(spriteProperty);
        mainContainer.Add(spriteField);

        serializedObject.ApplyModifiedProperties();
        #endregion
    }
    public LocationNodeEditorView(LocationNode data) : this()
    {
        guid = data.GUID;
        Position = data.Position;

        SetSprite(data.name);
    }

    private void SetSprite(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            spriteProperty.SetValue<Sprite>(Resources.Load<Sprite>(Location.TEXTURES_PATH + "/" + name));
        }
    }


    public override NodeData Data =>
        new LocationNode(guid, Position,
        spriteProperty.GetValue<Sprite>().name);
}


public class SpriteSO : ScriptableObject
{
    public Sprite sprite;
}

public class Texture2DSO : ScriptableObject
{
    public Texture2D texture2D;
}
