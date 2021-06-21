using System.Collections;
using UnityEngine;
using TMPro;

public class TextTyping : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrev;
    [SerializeField] private GameObject buttonNext;
    [SerializeField] private GameObject buttonStop;
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private float typingSpeed = 0.01f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip typingSound;

    private bool isTyping;
    private int lastSeenPageIndex;

    public delegate void eventDelegate();
    public event eventDelegate OnStopPageTyping;
    public event eventDelegate OnStopTyping;
    public event eventDelegate OnStartTyping;


    public void Type(string fullText)
    {
        textDisplay.pageToDisplay = 1;
        lastSeenPageIndex = 0;

        textDisplay.text = fullText;
        textDisplay.text = textDisplay.text.TrimEnd('\n', '\r', ' ');

        isTyping = true;
        SetupButtons();

        StopAllCoroutines();
        StartCoroutine(TypePageRoutine());
    }
    public void TypeOnNextPage(string additionalText)
    {
        textDisplay.text += "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n" + additionalText;
    }
    public void Stop()
    {
        textDisplay.text = "";

        isTyping = false;

        audioSource.Stop();

        StopAllCoroutines();

        OnStopTyping?.Invoke();
    }
    public void ShowPreviousPage()
    {
        textDisplay.pageToDisplay--;

        isTyping = false;

        StopAllCoroutines();

        SetupButtons();
    }
    public void ShowNextPage()
    {
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

        SetupButtons();
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

        OnStopPageTyping?.Invoke();
        if (index == textDisplay.pageToDisplay - 1)
        {
            OnStopTyping?.Invoke();
        }
    }
    private IEnumerator TypePageRoutine()
    {
        OnStartTyping?.Invoke();

        isTyping = true;

        audioSource.Stop();
        audioSource.PlayOneShot(typingSound);

        textDisplay.overflowMode = TextOverflowModes.Page;
        textDisplay.ForceMeshUpdate();
        textDisplay.SetAllDirty();

        int firstCharacterIndex = textDisplay.textInfo.pageInfo[textDisplay.pageToDisplay - 1].firstCharacterIndex;
        int lastCharacterIndex = textDisplay.textInfo.pageInfo[textDisplay.pageToDisplay - 1].lastCharacterIndex;

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

        SetupButtons();

        OnStopPageTyping?.Invoke();
    }
    private void SetupButtons()
    {
        textDisplay.ForceMeshUpdate();
        if (textDisplay.pageToDisplay == textDisplay.textInfo.pageCount && !isTyping)
        {
            buttonNext.SetActive(false);
            buttonStop.SetActive(true);

            OnStopTyping?.Invoke();
        }
        else
        {
            buttonNext.SetActive(true);
            buttonStop.SetActive(false);
        }

        if (textDisplay.pageToDisplay == 1)
        {
            buttonPrev.SetActive(false);
        }
        else
        {
            buttonPrev.SetActive(true);
        }
    }


    public bool IsTyping => isTyping;
}