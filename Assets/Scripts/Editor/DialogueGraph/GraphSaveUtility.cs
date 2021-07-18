using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;

public static class GraphSaveUtility
{
    public static readonly string DEFAULT_FOLDER = "Dialogues";


    public static void Save(DialogueGraphEditorView graphView, string path)
    {
        var entryNode = graphView.NodeDatas.Find(x => x is EntryNode);
        if (entryNode == null)
        {
            entryNode = graphView.EntryNode.Data;
        }

        DialogueGraphData dialogueData = ScriptableObject.CreateInstance<DialogueGraphData>();
        dialogueData.Init(entryNode.GUID, graphView.NodeDatas, graphView.NodeLinkDatas, graphView.ExposedVariables, graphView.CommentBlockDatas);

        AssetDatabaseExt.CreateOrReplaceAsset(dialogueData, path);
        AssetDatabase.SaveAssets();
        Debug.Log(graphView.name + " was saved");
    }
    public static DialogueGraphEditorView Load(DialogueGraphWindow graph, string filePath)
    {
        filePath = filePath.Replace("Assets/Resources/", "");
        filePath = System.IO.Path.ChangeExtension(filePath, null);

        DialogueGraphData dialogueData = Resources.Load<DialogueGraphData>(filePath);
        if (dialogueData == null)
        {
            EditorUtility.DisplayDialog("File Not Found", "Target Dialogue Graph data does not exist!", "OK");
            throw new Exception("File Not Found\nTarget Dialogue Graph data does not exist!");
        }

        DialogueGraphEditorView graphView = new DialogueGraphEditorView(graph);

        graphView.Clear();
        graphView.AddNodes(dialogueData.NodeDatas);
        graphView.ConnectNodes(dialogueData.NodeLinks);
        graphView.AddExposedProperties(dialogueData.ExposedVariables);
        graphView.GenerateCommentBlocks(dialogueData.CommentBlockDatas);

        return graphView;
    }
}