using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDayOrder : MonoBehaviour
{
    [SerializeField] private StoryTelling storyTelling;
    [SerializeField] private Story story;



    private bool isRunning = false;


    public void StartDay(int phraseIndex = 0)
    {
        storyTelling.StartStory(story, phraseIndex);

        isRunning = true;
    }


    public bool IsRunning => isRunning;
}
