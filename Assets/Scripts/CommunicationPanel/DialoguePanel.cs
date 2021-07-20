using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialoguePanel : CommunicationPanel
{
    private const string EMPTY_SPEAKER_NAME_EN = "Author";
    private const string EMPTY_SPEAKER_NAME_RU = "Автор";


    [SerializeField] private TextMeshProUGUI speakerName;
    [SerializeField] private Image background;
    [SerializeField] private Sprite panelWithoutSpeaker;
    [SerializeField] private Sprite panelWithSpeaker;
    [SerializeField] private TextTyping textTyping;
    [SerializeField] private ChoiceView choiceView;


    public void Show(string text, string speakerName, ChoiceData choiceData, List<UnityAction> actions)
    {
        fadeAnimation.Appear();

        textTyping.Type(text);
        choiceView.Show(choiceData, actions);
        SetSpeaker(speakerName);
    }

    private void SetSpeaker(string name)
    {
        if (string.IsNullOrEmpty(name) || name == EMPTY_SPEAKER_NAME_EN || name == EMPTY_SPEAKER_NAME_RU)
        {
            background.sprite = panelWithoutSpeaker;

            speakerName.text = "";
        }
        else
        {
            background.sprite = panelWithSpeaker;

            speakerName.text = name;
        }
    }
}
