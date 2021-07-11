using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDayControl : MonoBehaviour, IDataLoading, IDataSaving
{
    [SerializeField] private GameDayOrder gameDayOrder;
    [SerializeField] private DialogueGraphParser dialogueGraphParser;


    private int currentStoryIndex = 0;
    private int currentDayIndex = 0;
    private Story currectStory;
    private bool isRunning;


#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            EnterNextCharacter();
        }
    }
#endif

    public void StartDay()
    {
        currectStory = gameDayOrder.Stories[currentDayIndex];

        dialogueGraphParser.Parse(currectStory.Graph);
    }
    public void StartNewDay()
    {
        // ...

        StartDay();
    }
    public void FinishDay()
    {
        currentStoryIndex = 0;
        currentDayIndex++;

        StartNewDay();

        if (currentDayIndex < gameDayOrder.Stories.Count)
        {
            currectStory = gameDayOrder.Stories[currentDayIndex];
        }
    }
    public void EnterNextCharacter()
    {
        if (currentStoryIndex < gameDayOrder.Stories.Count)
        {
            dialogueGraphParser.Parse(currectStory.Graph);
        }
    }
    public void LoadData()
    {
        // currentCharacterIndex = Storage.GetData<GameDayData>().currentCharacterIndex;
        // currentDayIndex = Storage.GetData<GameDayData>().currentGameDayIndex;
    }
    public void SaveData()
    {
        // Storage.GetData<GameDayData>().currentCharacterIndex = currentCharacterIndex;
        // Storage.GetData<GameDayData>().currentGameDayIndex = currentDayIndex;
    }


    public int CurrentStoryIndex => currentStoryIndex;
    public int CurrentDayIndex => currentDayIndex;
    public bool IsRunning => isRunning;
}
