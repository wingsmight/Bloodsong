using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonologuePanel : CommunicationPanel
{
    private const string EMPTY_SPEAKER_NAME = "Author";


    [SerializeField] private TextShowing textShowing;
    [SerializeField] private GameObject speakerNameGameobject;
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
    public void SetSpeaker(string name)
    {
        if (speakerNameGameobject == null && speakerName == null)
            return;

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

    private IEnumerator ShowWithDelayRoutine(string text, string speakerName)
    {
        yield return new WaitForSeconds(appearingDelay);

        Show(text, speakerName);
    }
}
