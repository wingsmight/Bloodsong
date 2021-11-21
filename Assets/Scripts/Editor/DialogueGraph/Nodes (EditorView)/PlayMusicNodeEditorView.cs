using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayMusicNodeEditorView : NodeEditorView
{
    private SerializedProperty audioClipProperty;


    public PlayMusicNodeEditorView() : base()
    {
        title = "Play music";

        AddPort(Direction.Input, Port.Capacity.Multi);
        AddPort(Direction.Output, Port.Capacity.Single);

        #region audioClipField
        AudioClipSO audioClipSO = ScriptableObject.CreateInstance<AudioClipSO>();
        SerializedObject serializedObject = new SerializedObject(audioClipSO);
        audioClipProperty = serializedObject.FindProperty("audioClip");
        PropertyField audioClipField = new PropertyField(audioClipProperty, "Music:");
        audioClipField.BindProperty(audioClipProperty);
        mainContainer.Add(audioClipField);

        serializedObject.ApplyModifiedProperties();
        #endregion
    }
    public PlayMusicNodeEditorView(MusicNode data) : this()
    {
        guid = data.GUID;
        Position = data.Position;

        SetAudioClip(data.name);
    }

    private void SetAudioClip(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            audioClipProperty.SetValue<AudioClip>(Resources.Load<AudioClip>(AudioPlayer.MUSIC_PATH + "/" + name));
        }
    }


    public override NodeData Data =>
        new MusicNode(guid, Position,
        audioClipProperty.GetObjectReferenceValueName());
}


public class AudioClipSO : ScriptableObject
{
    public AudioClip audioClip;
}
