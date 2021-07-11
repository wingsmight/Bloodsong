using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ResponsesPanel : MonoBehaviour
{
    [SerializeField] private DialogueResponseView dialogueResponseViewPrefab;
    [SerializeField] private DialogueStatResponseView dialogueStatResponseViewPrefab;
    [SerializeField] private DialogueStatsResponseView dialogueStatsResponseViewPrefab;
    [SerializeField] private float appearingDelay = 0.05f;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextTyping textTyping;

    private List<DialogueResponseView> responseViews = new List<DialogueResponseView>();


    private void Awake()
    {
        Enable();
        textTyping.OnStopTyping += ShowChoices;
    }

    public void Clear()
    {
        DestroyResponses();
        responseViews = new List<DialogueResponseView>();
        StopAllCoroutines();
    }
    public void AddResponse(DialogueResponseData dialogueResponse, UnityAction action)
    {
        var newDialogueResponse = Instantiate<DialogueResponseView>(dialogueResponseViewPrefab, transform);
        newDialogueResponse.Init(dialogueResponse, () =>
        {
            Disable();
            action?.Invoke();
        });

        responseViews.Add(newDialogueResponse);
    }
    public void AddResponse(DialogueStatResponseData dialogueResponse, UnityAction action)
    {
        var newDialogueResponse = Instantiate<DialogueStatResponseView>(dialogueStatResponseViewPrefab, transform);
        newDialogueResponse.Init(dialogueResponse, () =>
        {
            Disable();
            action?.Invoke();
        });

        responseViews.Add(newDialogueResponse);
    }
    public void AddResponse(DialogueStatsResponseData dialogueResponse, UnityAction action)
    {
        var newDialogueResponse = Instantiate<DialogueStatsResponseView>(dialogueStatsResponseViewPrefab, transform);
        newDialogueResponse.Init(dialogueResponse, () =>
        {
            Disable();
            action?.Invoke();
        });

        responseViews.Add(newDialogueResponse);
    }
    public void ShowChoices()
    {
        StartCoroutine(ShowChoicesRoutine());
    }
    public void DestroyResponses()
    {
        transform.DestroyChildren();
        responseViews.Clear();
    }
    public void Disable()
    {
        canvasGroup.blocksRaycasts = false;
    }
    public void Enable()
    {
        canvasGroup.blocksRaycasts = true;
    }

    private IEnumerator ShowChoicesRoutine()
    {
        foreach (var responseView in responseViews)
        {
            responseView.Show();

            yield return new WaitForSeconds(appearingDelay);
        }
    }
}
