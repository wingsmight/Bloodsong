using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public abstract class TextShowing : MonoBehaviour, IShowPaging
{
    [SerializeField] private List<PrevPageButton> prevPageControls;
    [SerializeField] private List<NextPageButton> nextPageControls;
    [SerializeField] private List<StopPageButton> stopPageControls;
    [SerializeField] protected TextMeshProUGUI textDisplay;


    protected bool isTyping;


    public event UnityAction OnStopPageTyping;
    public event UnityAction OnStopTyping;
    public event UnityAction OnStartTyping;


    public abstract void Type(string fullText);
    public abstract void TypeOnNextPage(string additionalText);
    public abstract void Stop();
    public abstract void ShowPreviousPage();
    public abstract void ShowNextPage();

    protected virtual void InvokeOnStopPageTyping()
    {
        OnStopPageTyping?.Invoke();
    }
    protected virtual void InvokeOnStopTyping()
    {
        OnStopTyping?.Invoke();
    }
    protected virtual void InvokeOnStartTyping()
    {
        OnStartTyping?.Invoke();
    }

    protected void SetupPageControls()
    {
        textDisplay.ForceMeshUpdate();
        if (textDisplay.pageToDisplay == textDisplay.textInfo.pageCount && !isTyping)
        {
            nextPageControls?.ForEach(x => x.SetActive(false));
            stopPageControls?.ForEach(x => x.SetActive(true));

            InvokeOnStopTyping();
        }
        else
        {
            nextPageControls?.ForEach(x => x.SetActive(true));
            stopPageControls?.ForEach(x => x.SetActive(false));
        }

        if (textDisplay.pageToDisplay == 1)
        {
            prevPageControls?.ForEach(x => x.SetActive(false));
        }
        else
        {
            prevPageControls?.ForEach(x => x.SetActive(true));
        }
    }


    public bool IsTyping => isTyping;
}
