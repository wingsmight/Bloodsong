using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueGraphWindow : EditorWindow
{
    private DialogueGraphEditorView graphView;
    private string currentFilePath;
    private Toolbar toolbar;
    private MiniMap miniMap;
    private Blackboard blackboard;


    [MenuItem("Dialogue/Dialogue graph")]
    public static void CreateGraphViewWindow()
    {
        var window = GetWindow<DialogueGraphWindow>();
        window.titleContent = new GUIContent("Dialogue graph");
    }


    private void OnEnable()
    {
        var graphView = new DialogueGraphEditorView(this);
        SetView(graphView);
        GenerateEnvironment();
    }
    private void OnDisable()
    {
        rootVisualElement.Remove(graphView);
    }

    private void SetView(DialogueGraphEditorView graphView)
    {
        if (this.graphView != null)
        {
            rootVisualElement.Remove(this.graphView);
        }

        this.graphView = graphView;
        this.graphView.name = "Narrative graph";
        this.graphView.StretchToParentSize();
        rootVisualElement.Add(this.graphView);
    }
    private void GenerateEnvironment()
    {
        GenerateToolbar();
        GenerateMiniMap();
        GenerateBlackBoard();
    }
    private void GenerateToolbar()
    {
        if (toolbar != null && rootVisualElement.Children().Any(x => x == toolbar))
        {
            rootVisualElement.Add(toolbar);
        }
        else
        {
            toolbar = new Toolbar();

            Button saveButton = new Button(() =>
            {
                Save(currentFilePath);
            });
            saveButton.text = "Save";
            saveButton.visible = false;

            LoadedDialogue obj = ScriptableObject.CreateInstance<LoadedDialogue>();
            SerializedObject serializedObject = new SerializedObject(obj);
            SerializedProperty property = serializedObject.FindProperty("dialogue");
            PropertyField loadProperty = new PropertyField(property, "Load dialogue:");

            loadProperty.RegisterCallback<ChangeEvent<UnityEngine.Object>>(evt =>
            {
                if (evt?.newValue != null)
                {
                    string assetPath = AssetDatabase.GetAssetPath(evt?.newValue);
                    Load(assetPath);

                    saveButton.visible = true;
                }
                else
                {
                    CreateGraphView(new DialogueGraphEditorView(this));
                    obj.dialogue = null;

                    saveButton.visible = false;
                }
            });
            loadProperty.BindProperty(property);
            loadProperty.MarkDirtyRepaint();

            Button createNewGraph = new Button(() =>
            {
                CreateGraphView(new DialogueGraphEditorView(this));
                obj.dialogue = null;
            });
            createNewGraph.text = "Create new graph";

            ToolbarSpacer space = new ToolbarSpacer();
            space.flex = true;

            Button saveAsButton = new Button(() =>
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(currentFilePath);
                string newPath = EditorUtility.SaveFilePanelInProject("Save dialogue as", fileName, "asset", "message", "Assets/Resources/" + GraphSaveUtility.DEFAULT_FOLDER);
                if (!string.IsNullOrEmpty(newPath))
                {
                    Save(newPath);

                    obj.dialogue = AssetDatabase.LoadAssetAtPath<DialogueGraphData>(newPath);
                }
                else
                {
                    Debug.LogWarning("File wasn't saved because the File panel was closed or canceled");
                }
            });
            saveAsButton.text = "Save as";

            toolbar.Add(createNewGraph);
            toolbar.Add(loadProperty);
            toolbar.Add(space);
            toolbar.Add(saveButton);
            toolbar.Add(saveAsButton);

            rootVisualElement.Add(toolbar);
        }
    }
    private void GenerateMiniMap()
    {
        miniMap = new MiniMap();
        miniMap.anchored = true;
        miniMap.SetPosition(new Rect(2, 22, 200, 140));
        graphView.Add(miniMap);
    }
    private void GenerateBlackBoard()
    {
        blackboard = new Blackboard(graphView);
        blackboard.Add(new BlackboardSection { title = "Exposed Variables" });
        blackboard.addItemRequested = _blackboard =>
        {
            graphView.AddVariableToBlackBoard(new ExposedVariable());
        };
        blackboard.editTextRequested = (_blackboard, element, newValue) =>
        {
            var oldPropertyName = ((BlackboardField)element).text;
            if (graphView.ExposedVariables.Any(x => x.Name == newValue))
            {
                EditorUtility.DisplayDialog("Error", "This property name already exists, please chose another one.", "OK");
                return;
            }

            var targetIndex = graphView.ExposedVariables.FindIndex(x => x.Name == oldPropertyName);
            graphView.ExposedVariables[targetIndex].Name = newValue;
            ((BlackboardField)element).text = newValue;
        };
        blackboard.SetPosition(new Rect(0, 500, 200, 300));
        graphView.Add(blackboard);
        graphView.Blackboard = blackboard;
    }
    private void RemoveMiniMap()
    {
        if (miniMap != null)
        {
            graphView.Remove(miniMap);
        }
    }
    private void RemoveBlackBoard()
    {
        if (blackboard != null)
        {
            graphView.Remove(blackboard);
        }
    }
    private void CreateGraphView(DialogueGraphEditorView graphView)
    {
        RemoveMiniMap();
        RemoveBlackBoard();
        SetView(graphView);
        GenerateEnvironment();
    }

    private void Save(string filePath)
    {
        if (!string.IsNullOrEmpty(filePath))
        {
            currentFilePath = filePath;

            GraphSaveUtility.Save(graphView, filePath);
        }
    }
    private void Load(string filePath)
    {
        if (!string.IsNullOrEmpty(filePath))
        {
            currentFilePath = filePath;

            var graphView = GraphSaveUtility.Load(this, filePath);

            CreateGraphView(graphView);
        }
    }
}

public class LoadedDialogue : ScriptableObject
{
    public DialogueGraphData dialogue;
}