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


    private int currentStoryIndex = 0;
    private Story currectStory;
    private bool isRunning;
    private string currentNodeGuid;


    public void StartDay()
    {
        ResetableViews.ForEach(x => x.Reset());

        SaveLoadLauncher.Instance.LoadDatas();

        isRunning = true;

        currectStory = gameDayOrder.Stories[currentStoryIndex];
        dialogueGraphParser.Parse(currectStory.Graph, currentNodeGuid);

        inGameMenuAnimation.Appear();
    }
    public void LoadData()
    {
        currentStoryIndex = Storage.GetData<GameDayData>().currentStoryIndex;
        currentNodeGuid = Storage.GetData<GameDayData>().currentNodeGuid;
    }
    public void SaveData()
    {
        Storage.GetData<GameDayData>().currentStoryIndex = currentStoryIndex;
        Storage.GetData<GameDayData>().currentNodeGuid = dialogueGraphParser.CurrentNode.guid;
    }


    public int CurrentStoryIndex => currentStoryIndex;
    public bool IsRunning => isRunning;

    private List<IResetable> ResetableViews => resetableViews.Cast<IResetable>().ToList();
}
