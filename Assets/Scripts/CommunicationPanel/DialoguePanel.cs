using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : CommunicationPanel
{
    [SerializeField] private DialogueGraphParser dialogueParser;


    public void StartConversationWithDelay(DialogueGraphData dialogue)
    {
        StartCoroutine(StartConversationWithDelayRoutine(dialogue));
    }
    public void StartConversationWithDelay(DialogueGraphData dialogue, float delay)
    {
        StartCoroutine(StartConversationWithDelayRoutine(dialogue, delay));
    }
    public void StartConversation(DialogueGraphData dialogue)
    {
        dialogueParser.Parse(dialogue);

        fadeAnimation.Appear();
    }

    private IEnumerator StartConversationWithDelayRoutine(DialogueGraphData dialogue)
    {
        yield return new WaitForSeconds(appearingDelay);

        dialogueParser.Parse(dialogue);

        fadeAnimation.Appear();
    }
    private IEnumerator StartConversationWithDelayRoutine(DialogueGraphData dialogue, float delay)
    {
        yield return new WaitForSeconds(delay);

        dialogueParser.Parse(dialogue);

        fadeAnimation.Appear();
    }
}
