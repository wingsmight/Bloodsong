using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialoguePortChoiceMenu : ScriptableObject, ISearchWindowProvider
{
    private DialogueNodeEditorView dialogueNode;
    private Texture2D indentationIcon;


    public void Init(DialogueNodeEditorView dialogueNode)
    {
        this.dialogueNode = dialogueNode;

        indentationIcon = new Texture2D(1, 1);
        indentationIcon.SetPixel(0, 0, new Color(0, 0, 0, 0));
        indentationIcon.Apply();
    }
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        var tree = new List<SearchTreeEntry>
            {
                new SearchTreeGroupEntry(new GUIContent("Create port"), 0),
                new SearchTreeEntry(new GUIContent("Text", indentationIcon))
                {
                    level = 1,
                    userData = new DialogueResponseData(),
                },
                new SearchTreeEntry(new GUIContent("Stat & text", indentationIcon))
                {
                    level = 1,
                    userData = new DialogueStatResponseData(),
                },
                new SearchTreeEntry(new GUIContent("Stats & text", indentationIcon))
                {
                    level = 1,
                    userData = new DialogueStatsResponseData(),
                },
            };

        return tree;
    }
    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        if (SearchTreeEntry.userData is DialogueStatResponseData)
        {
            dialogueNode.AddResponsePort(SearchTreeEntry.userData as DialogueStatResponseData);

            return true;
        }
        else if (SearchTreeEntry.userData is DialogueStatsResponseData)
        {
            dialogueNode.AddResponsePort(SearchTreeEntry.userData as DialogueStatsResponseData);

            return true;
        }
        else if (SearchTreeEntry.userData is DialogueResponseData)
        {
            dialogueNode.AddResponsePort(SearchTreeEntry.userData as DialogueResponseData);

            return true;
        }
        else
        {
            return false;
        }
    }
}