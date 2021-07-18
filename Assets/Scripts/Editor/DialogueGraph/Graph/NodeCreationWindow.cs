using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class NodeCreationWindow : ScriptableObject, ISearchWindowProvider
{
    private EditorWindow window;
    private DialogueGraphEditorView graphView;
    private Texture2D indentationIcon;


    public void Init(EditorWindow window, DialogueGraphEditorView graphView)
    {
        this.window = window;
        this.graphView = graphView;

        indentationIcon = new Texture2D(1, 1);
        indentationIcon.SetPixel(0, 0, new Color(0, 0, 0, 0));
        indentationIcon.Apply();
    }
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        var tree = new List<SearchTreeEntry>
            {
                new SearchTreeGroupEntry(new GUIContent("Create Node"), 0),

                new SearchTreeGroupEntry(new GUIContent("Create Node"), 1),
                new SearchTreeEntry(new GUIContent("Dialogue Node", indentationIcon))
                {
                    level = 2,
                    userData = new DialogueNodeEditorView()
                },
                new SearchTreeEntry(new GUIContent("Stop Node", indentationIcon))
                {
                    level = 2,
                    userData = new StopNodeEditorView()
                },
                new SearchTreeEntry(new GUIContent("Random output Node", indentationIcon))
                {
                    level = 2,
                    userData = new RandomOutputNodeEditorView()
                },
                new SearchTreeEntry(new GUIContent("Start monologue", indentationIcon))
                {
                    level = 2,
                    userData = new StartMonologueEditorNodeView()
                },
                new SearchTreeEntry(new GUIContent("Start split message", indentationIcon))
                {
                    level = 2,
                    userData = new StartSplitMessageEditorNodeView()
                },
                new SearchTreeEntry(new GUIContent("Show location", indentationIcon))
                {
                    level = 2,
                    userData = new LocationNodeEditorView()
                },

                new SearchTreeGroupEntry(new GUIContent("Characters"), 2),
                new SearchTreeEntry(new GUIContent("Show character", indentationIcon))
                {
                    level = 3,
                    userData = new ShowCharacterNodeEditorView()
                },


                new SearchTreeEntry(new GUIContent("Comment Block", indentationIcon))
                {
                    level = 1,
                    userData = new Group()
                },

            };

        return tree;
    }
    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        var mousePosition = window.rootVisualElement.ChangeCoordinatesTo(window.rootVisualElement.parent, context.screenMousePosition - window.position.position);
        var graphMousePosition = graphView.contentViewContainer.WorldToLocal(mousePosition);

        if (SearchTreeEntry.userData is Group)
        {
            graphView.AddCommentBlock(new CommentBlockData(), graphMousePosition);

            return true;
        }
        else if (SearchTreeEntry.userData is NodeEditorView)
        {
            (SearchTreeEntry.userData as NodeEditorView).Position = graphMousePosition;
            graphView.AddElement(SearchTreeEntry.userData as NodeEditorView);

            return true;
        }
        else
        {
            return false;
        }
    }
}