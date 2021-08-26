using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialoguePanel : CommunicationPanel
{
    [SerializeField] private TextTyping textTyping;
    [SerializeField] private ChoiceView choiceView;
    [SerializeField] private SpeakerView speaker;
    [Space(12)]
    [SerializeField] private int indentPercent;


    public void Show(string text, string speakerName, ChoiceData choiceData, List<UnityAction> actions)
    {
        fadeAnimation.Appear();

        text = text.RemoveAllOccurrences('\n');
        text = text.AddIndentTag(indentPercent);
        textTyping.Type(text);
        choiceView.Show(choiceData, actions);
        speaker.Show(speakerName);
    }
}
