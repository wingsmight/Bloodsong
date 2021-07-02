using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDayOrder : MonoBehaviour
{
    [SerializeField] private StoryTelling storyTelling;
    [SerializeField] private Story story;


    public void StartDay(int phraseIndex = 0)
    {
        storyTelling.StartStory(story, phraseIndex);
    }
}
