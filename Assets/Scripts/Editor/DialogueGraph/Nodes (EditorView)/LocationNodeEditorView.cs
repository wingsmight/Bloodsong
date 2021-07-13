using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class LocationNodeEditorView : NodeEditorView
{
    private SerializedProperty spriteProperty;
    private SerializedProperty texture2DProperty;


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

        #region texture2DField
        Texture2DSO texture2DSO = ScriptableObject.CreateInstance<Texture2DSO>();
        SerializedObject serializedTexture2D = new SerializedObject(texture2DSO);
        texture2DProperty = serializedTexture2D.FindProperty("texture2D");
        PropertyField texture2DField = new PropertyField(texture2DProperty, "Texture 2D:");
        texture2DField.BindProperty(texture2DProperty);
        mainContainer.Add(texture2DField);

        serializedTexture2D.ApplyModifiedProperties();
        #endregion
    }
    public LocationNodeEditorView(LocationNode data) : this()
    {
        guid = data.GUID;
        Position = data.Position;

        SetSprite(data.name);
        SetTexture2D(data.name);
    }

    private void SetSprite(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            //spriteProperty.SetValue<Sprite>(Resources.Load<Texture2D>(Location.TEXTURES_PATH + "/" + name));
        }
    }
    private void SetTexture2D(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            spriteProperty.SetValue<Texture2D>(Resources.Load<Texture2D>(Location.TEXTURES_PATH + "/" + name));
        }
    }


    public override NodeData Data =>
        new LocationNode(guid, Position,
        //texture2DProperty.GetValue<Texture2D>()
        null);
}


public class SpriteSO : ScriptableObject
{
    public Sprite sprite;
}

public class Texture2DSO : ScriptableObject
{
    public Texture2D texture2D;
}
