using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonologuePanel : CommunicationPanel
{
    [SerializeField] private TextTyping textTyping;
    [SerializeField] private TextMeshProUGUI stopMonologueText;
    [SerializeField] private GameObject speakerNameGameobject;
    [SerializeField] private TextMeshProUGUI speakerName;


    public void StartConversation(string text)
    {
        fadeAnimation.Appear();
        textTyping.Type(text);
    }
    public void StartConversationWithDelay(string text)
    {
        StartCoroutine(StartConversationWithDelayRoutine(text));
    }
    public void OverrideStopText(string newText)
    {
        stopMonologueText.text = newText;
    }
    public void SetSpeaker(Speaker speaker)
    {
        speakerNameGameobject.SetActive(!string.IsNullOrEmpty(speaker.name));
        speakerName.text = speaker.name;
    }

    private IEnumerator StartConversationWithDelayRoutine(string text)
    {
        yield return new WaitForSeconds(appearingDelay);

        StartConversation(text);
    }
}
