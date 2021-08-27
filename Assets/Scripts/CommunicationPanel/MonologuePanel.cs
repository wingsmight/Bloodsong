using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonologuePanel : CommunicationPanel
{
    [SerializeField] private TextShowing textShowing;
    [SerializeField] private SpeakerView speaker;
    [SerializeField] private DialogueGraphParser graphParser;
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
    public override void Hide()
    {
        base.Hide();

        var nextNode = graphParser.CurrentDialogue.GetNextNodes(graphParser.CurrentNode)[0];
        if (!(nextNode is MonologueNode) || SpeakerView.IsEmptySpeaker((nextNode as MonologueNode).speakerName))
        {
            speaker.Hide();
        }
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
