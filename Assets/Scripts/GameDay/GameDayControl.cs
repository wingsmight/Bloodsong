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
    [SerializeField] private FadeAnimation inGameMenuAnimation;
    [SerializeField] [RequireInterface(typeof(IResetable))] private List<UnityEngine.Object> resetableViews;
    [SerializeField] private StartMonologueNodeView monologueNodeView;
    [SerializeField] private StopNodeView stopNodeView;


    private Story currectStory;
    private string currentNodeGuid;
    private int currentStoryIndex = 0;
    private int currentStoryPhraseIndex = 0;
    private bool isRunning;


    private void Awake()
    {
        stopNodeView.OnGraphEnded += () => StartDay(currentStoryIndex + 1);
    }

    public void StartDay()
    {
        ResetableViews.ForEach(x => x.Reset());

        SaveLoadLauncher.Instance.LoadDatas();

        isRunning = true;

        StartDay(currentStoryIndex);

        inGameMenuAnimation.Appear();
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
    }
    public void SaveData()
    {
        Storage.GetData<GameDayData>().currentNodeGuid = dialogueGraphParser.CurrentNode?.guid;
        Storage.GetData<GameDayData>().currentStoryIndex = currentStoryIndex;
        Storage.GetData<GameDayData>().currentStoryPhraseIndex = monologueNodeView.PhraseIndex;
    }


    public int CurrentStoryIndex => currentStoryIndex;
    public bool IsRunning => isRunning;

    private List<IResetable> ResetableViews => resetableViews.Cast<IResetable>().ToList();
}
