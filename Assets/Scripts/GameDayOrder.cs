using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDayOrder : MonoBehaviour
{
    [SerializeField] private StoryTelling storyTelling;
    [SerializeField] private Story story;
    // test
    [Space(12)]
    [SerializeField] private SplitTextAppearing splitTextAppearing;


    private bool isRunning = false;


    public void StartDay(int phraseIndex = 0)
    {
        // test
        //storyTelling.StartStory(story, phraseIndex);
        int phrasesCount = 2;
        var phrases = new List<string>();
        for (int i = 0; i < phrasesCount; i++)
        {
            phrases.Add(story.Phrases[i].text);
        }
        splitTextAppearing.Type(phrases);

        isRunning = true;
    }


    public bool IsRunning => isRunning;
}
