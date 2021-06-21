using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDayOrder : MonoBehaviour
{
    [SerializeField] private StoryTelling storyTelling;
    [SerializeField] private Story story;


    public void StartDay()
    {
        storyTelling.StartStory(story);
    }
}
