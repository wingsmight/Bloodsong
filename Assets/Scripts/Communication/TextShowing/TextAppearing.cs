using System.Collections;
using UnityEngine;
using TMPro;

public class TextAppearing : TextShowing
{
    [SerializeField] private FadeAnimation fadeAnimation;


    private int lastSeenPageIndex;


    public override void Type(string fullText)
    {
        textDisplay.pageToDisplay = 1;
        lastSeenPageIndex = 0;

        textDisplay.text = fullText;
        textDisplay.text = textDisplay.text.TrimEnd('\n', '\r', ' ');

        isTyping = true;
        SetupPageControls();

        StopAllCoroutines();
        StartCoroutine(TypePageRoutine());
    }
    public override void TypeOnNextPage(string additionalText)
    {
        textDisplay.text += "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n" + additionalText;
    }
    public override void Stop()
    {
        textDisplay.text = "";

        isTyping = false;

        StopAllCoroutines();

        InvokeOnStopTyping();
    }
    public override void ShowPreviousPage()
    {
        textDisplay.pageToDisplay--;

        isTyping = false;

        StopAllCoroutines();

        SetupPageControls();
    }
    public override void ShowNextPage()
    {
        if (textDisplay.pageToDisplay == textDisplay.textInfo.pageCount)
        {
            Stop();

            return;
        }

        if (isTyping)
        {
            ShowPage(textDisplay.pageToDisplay);
        }
        else
        {
            textDisplay.pageToDisplay++;

            if (lastSeenPageIndex == textDisplay.pageToDisplay - 1)
            {
                StartCoroutine(TypePageRoutine());
            }
            else
            {
                ShowPage(textDisplay.pageToDisplay);
            }
        }

        SetupPageControls();
    }

    private void ShowPage(int index)
    {
        textDisplay.pageToDisplay = index;

        StopAllCoroutines();

        fadeAnimation.SetVisible(true);

        if (lastSeenPageIndex == textDisplay.pageToDisplay - 1)
        {
            lastSeenPageIndex = textDisplay.pageToDisplay;
        }

        isTyping = false;

        InvokeOnStopPageTyping();
        if (index == textDisplay.pageToDisplay - 1)
        {
            InvokeOnStopTyping();
        }
    }
    private IEnumerator TypePageRoutine()
    {
        InvokeOnStartTyping();

        isTyping = true;

        textDisplay.overflowMode = TextOverflowModes.Page;
        textDisplay.ForceMeshUpdate();
        textDisplay.SetAllDirty();

        fadeAnimation.Appear();
        yield return new WaitUntil(() => fadeAnimation.IsShowing);

        isTyping = false;

        SetupPageControls();

        InvokeOnStopPageTyping();
    }
}