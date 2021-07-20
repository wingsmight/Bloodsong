using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonologuePanel : CommunicationPanel
{
    private const string EMPTY_SPEAKER_NAME_EN = "Author";
    private const string EMPTY_SPEAKER_NAME_RU = "Автор";


    [SerializeField] private TextShowing textShowing;
    [SerializeField] private Image background;
    [SerializeField] private Sprite panelWithoutSpeaker;
    [SerializeField] private Sprite panelWithSpeaker;
    [SerializeField] private TextMeshProUGUI speakerName;


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
        textShowing.Type(text);
        SetSpeaker(speakerName);
    }
    public void ShowWithDelay(string text, string speakerName)
    {
        StartCoroutine(ShowWithDelayRoutine(text, speakerName));
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
    private IEnumerator ShowWithDelayRoutine(string text, string speakerName)
    {
        yield return new WaitForSeconds(appearingDelay);

        Show(text, speakerName);
    }
}
