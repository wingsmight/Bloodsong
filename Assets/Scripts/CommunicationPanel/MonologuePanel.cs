using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonologuePanel : CommunicationPanel
{
    [SerializeField] private TextShowing textShowing;
    [SerializeField] private TextMeshProUGUI stopMonologueText;
    [SerializeField] private GameObject speakerNameGameobject;
    [SerializeField] private TextMeshProUGUI speakerName;


    public void StartConversation(string text, Character speaker)
    {
        StartConversation(text, speaker.Name);
    }
    public void StartConversation(string text, string speakerName)
    {
        fadeAnimation.Appear();
        textShowing.Type(text);
        SetSpeaker(speakerName);
    }
    public void StartConversationWithDelay(string text, string speakerName)
    {
        StartCoroutine(StartConversationWithDelayRoutine(text, speakerName));
    }
    public void OverrideStopText(string newText)
    {
        stopMonologueText.text = newText;
    }
    public void SetSpeaker(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            speakerNameGameobject.SetActive(false);
        }
        else
        {
            speakerNameGameobject.SetActive(!string.IsNullOrEmpty(name));
            speakerName.text = name;
        }
    }

    private IEnumerator StartConversationWithDelayRoutine(string text, string speakerName)
    {
        yield return new WaitForSeconds(appearingDelay);

        StartConversation(text, speakerName);
    }
}
