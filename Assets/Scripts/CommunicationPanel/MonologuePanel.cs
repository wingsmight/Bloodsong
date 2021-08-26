using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonologuePanel : CommunicationPanel
{
    [SerializeField] private TextShowing textShowing;
    [SerializeField] private SpeakerView speaker;
    [Space(12)]
    [SerializeField] private int indentPercent;


    public void Show(string text)
    {
        Show(text, "");
    }
    public void Show(string text, Character speaker)
    {
        Show(text, speaker?.Name);
    }
    public void Show(string text, string speakerName)
    {
        fadeAnimation.Appear();

        text = text.RemoveAllOccurrences('\n');
        text = text.AddIndentTag(indentPercent);
        textShowing.Type(text);
        speaker.Show(speakerName);
    }
    public void ShowWithDelay(string text, string speakerName)
    {
        StartCoroutine(ShowWithDelayRoutine(text, speakerName));
    }

    private IEnumerator ShowWithDelayRoutine(string text, string speakerName)
    {
        yield return new WaitForSeconds(appearingDelay);

        Show(text, speakerName);
    }
}
