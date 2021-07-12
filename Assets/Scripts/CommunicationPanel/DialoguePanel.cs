using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialoguePanel : CommunicationPanel
{
    private const string EMPTY_SPEAKER_NAME = "Author";


    [SerializeField] private GameObject speakerNameGameobject;
    [SerializeField] private TextMeshProUGUI speakerName;
    [SerializeField] private TextTyping textTyping;
    [SerializeField] private ChoiceView choiceView;


    public void StartConversation(string text, string speakerName, ChoiceData choiceData, List<UnityAction> actions)
    {
        fadeAnimation.Appear();

        textTyping.Type(text);
        choiceView.Show(choiceData, actions);
        SetSpeaker(speakerName);
    }
    public void SetSpeaker(string name)
    {
        if (string.IsNullOrEmpty(name) || name == EMPTY_SPEAKER_NAME)
        {
            speakerNameGameobject.SetActive(false);
        }
        else
        {
            speakerNameGameobject.SetActive(!string.IsNullOrEmpty(name));
            speakerName.text = name;
        }
    }
}
