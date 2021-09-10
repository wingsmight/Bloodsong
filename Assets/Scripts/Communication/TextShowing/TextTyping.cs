using System.Collections;
using UnityEngine;
using TMPro;

public class TextTyping : TextShowing
{
    private const float MIN_ACCEPTABLE_SPEED = 0.0f;


    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip typingSound;


    private static float typingSpeed = 0.01f;
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

        audioSource.Stop();

        StopAllCoroutines();

        InvokeOnStopTyping();
    }
    public override void ShowPreviousPage()
    {
        if (textDisplay.pageToDisplay > 1)
        {
            textDisplay.pageToDisplay--;
        }
        else
        {
            Stop();
        }

        isTyping = false;

        StopAllCoroutines();

        SetupPageControls();
    }
    public override void ShowNextPage()
    {
        if (isTyping)
        {
            ShowPage(textDisplay.pageToDisplay);
        }
        else
        {
            if (textDisplay.textInfo.pageCount == 0)
            {
                textDisplay.pageToDisplay = 1;
                return;
            }
            else if (textDisplay.pageToDisplay >= textDisplay.textInfo.pageCount)
            {
                textDisplay.pageToDisplay = textDisplay.textInfo.pageCount;
                return;
            }
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

        int lastCharacterIndex = textDisplay.textInfo.pageInfo[index - 1].lastCharacterIndex;

        textDisplay.maxVisibleCharacters = lastCharacterIndex + 1;

        if (lastSeenPageIndex == textDisplay.pageToDisplay - 1)
        {
            lastSeenPageIndex = textDisplay.pageToDisplay;
        }

        audioSource.Stop();

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

        audioSource.Stop();
        if (typingSound != null)
        {
            audioSource.PlayOneShot(typingSound);
        }

        textDisplay.overflowMode = TextOverflowModes.Page;
        textDisplay.ForceMeshUpdate();
        textDisplay.SetAllDirty();

        int firstCharacterIndex = 0;
        int lastCharacterIndex = 0;
        if (textDisplay.pageToDisplay <= textDisplay.textInfo.pageCount)
        {
            firstCharacterIndex = textDisplay.textInfo.pageInfo[textDisplay.pageToDisplay - 1].firstCharacterIndex;
            lastCharacterIndex = textDisplay.textInfo.pageInfo[textDisplay.pageToDisplay - 1].lastCharacterIndex;
        }


        if (typingSpeed - MIN_ACCEPTABLE_SPEED < float.Epsilon)
        {
            firstCharacterIndex = lastCharacterIndex;
        }

        for (int i = firstCharacterIndex; i <= lastCharacterIndex; i++)
        {
            textDisplay.maxVisibleCharacters = i + 1;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (lastSeenPageIndex == textDisplay.pageToDisplay - 1)
        {
            lastSeenPageIndex = textDisplay.pageToDisplay;
        }

        audioSource.Stop();

        isTyping = false;

        SetupPageControls();

        InvokeOnStopPageTyping();
    }


    public static float Speed { get => typingSpeed; set => typingSpeed = value; }
}