using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDayControl : MonoBehaviour, IDataLoading, IDataSaving
{
    [SerializeField] private GameDayOrder gameDayOrder;
    [SerializeField] private DialogueGraphParser dialogueGraphParser;
    [SerializeField] private FadeAnimation inGameMenuAnimation;


    private int currentStoryIndex = 0;
    private Story currectStory;
    private bool isRunning;
    private NodeData currentNode;


    public void StartDay()
    {
        isRunning = true;

        currectStory = gameDayOrder.Stories[currentStoryIndex];
        dialogueGraphParser.Parse(currectStory.Graph, currentNode);

        inGameMenuAnimation.Appear();
    }
    public void LoadData()
    {
        currentStoryIndex = Storage.GetData<GameDayData>().currentStoryIndex;
        currentNode = Storage.GetData<GameDayData>().currentNode;
        if (currentNode == null || string.IsNullOrEmpty(currentNode.GUID))
        {
            currentNode = null;
        }
    }
    public void SaveData()
    {
        Storage.GetData<GameDayData>().currentStoryIndex = currentStoryIndex;
        Storage.GetData<GameDayData>().currentNode = dialogueGraphParser.CurrentNode;
    }


    public int CurrentStoryIndex => currentStoryIndex;
    public bool IsRunning => isRunning;
}
