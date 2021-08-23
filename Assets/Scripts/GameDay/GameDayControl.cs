using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDayControl : MonoBehaviour, IDataLoading, IDataSaving
{
    [SerializeField] private GameDayOrder gameDayOrder;
    [SerializeField] private DialogueGraphParser dialogueGraphParser;
    [SerializeField] [RequireInterface(typeof(IResetable))] private List<UnityEngine.Object> resetableViews;
    [SerializeField] private StartMonologueNodeView monologueNodeView;
    [SerializeField] private StopNodeView stopNodeView;


    private Story currectStory;
    private string currentNodeGuid;
    private int currentStoryIndex = 0;
    private int currentStoryPhraseIndex = 0;
    private BranchNodesStack branchNodes;


    private void Awake()
    {
        stopNodeView.OnGraphEnded += StartNextDay;
    }
    private void OnDestroy()
    {
        stopNodeView.OnGraphEnded -= StartNextDay;
    }
    private void Start()
    {
        // Reset isn't necessary
        //ResetableViews.ForEach(x => x.Reset());

        StartDay(currentStoryIndex);
    }

    public void StartDay(int storyIndex)
    {
        currentStoryIndex = storyIndex;

        if (currentStoryIndex >= gameDayOrder.Stories.Count)
        {
            return;
        }

        currectStory = gameDayOrder.Stories[currentStoryIndex];
        var graph = currectStory.Graph;
        var currentNode = graph.GetNodeByGUID(currentNodeGuid);
        dialogueGraphParser.CurrentDialogue = graph;
        dialogueGraphParser.CurrentNode = currentNode;
        if (currentNode is MonologueNode)
        {
            monologueNodeView.Act(graph, currentNode as MonologueNode, currentStoryPhraseIndex);
        }
        else
        {
            dialogueGraphParser.Parse(graph, currentNode);
        }
    }
    public void LoadData()
    {
        currentNodeGuid = Storage.GetData<GameDayData>().currentNodeGuid;
        currentStoryIndex = Storage.GetData<GameDayData>().currentStoryIndex;
        currentStoryPhraseIndex = Storage.GetData<GameDayData>().currentStoryPhraseIndex;
        branchNodes = new BranchNodesStack(Storage.GetData<GameDayData>().branchNodes);
    }
    public void SaveData()
    {
        Storage.GetData<GameDayData>().currentNodeGuid = dialogueGraphParser.CurrentNode?.guid;
        Storage.GetData<GameDayData>().currentStoryIndex = currentStoryIndex;
        Storage.GetData<GameDayData>().currentStoryPhraseIndex = monologueNodeView.PhraseIndex;
        Storage.GetData<GameDayData>().branchNodes = new BranchNodesStack(branchNodes);
    }

    private void StartNextDay()
    {
        currentStoryIndex++;
        StartDay(currentStoryIndex);
    }


    public int CurrentStoryPhraseIndex { get => currentStoryPhraseIndex; set => currentStoryPhraseIndex = value; }
    public int CurrentStoryIndex => currentStoryIndex;
    public BranchNodesStack BranchNodes => branchNodes;
    public DialogueGraphData PrevDialogue => currentStoryIndex > 0 ? gameDayOrder.Stories[currentStoryIndex - 1].Graph : null;

    private List<IResetable> ResetableViews => resetableViews.Cast<IResetable>().ToList();
}
