using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SplitTextAppearingSet : MonoBehaviour, IShowPaging, IResetable
{
    [SerializeField] protected List<TextAppearing> textAppearings;
    [SerializeField] protected List<NextPageButton> nextPageControls;
    [SerializeField] protected List<StopPageButton> stopPageControls;


    public event UnityAction OnStopPageTyping;
    public event UnityAction OnStopTyping;
    public event UnityAction OnStartTyping;

    protected int textIndex;
    protected List<string> fullTexts;


    private void Awake()
    {
        for (int i = 0; i < textAppearings.Count; i++)
        {
            textAppearings[i].OnStartTyping += () => OnStartTyping?.Invoke();
        }
        for (int i = 0; i < textAppearings.Count; i++)
        {
            textAppearings[i].OnStopPageTyping += () => OnStopPageTyping?.Invoke();
        }
        for (int i = 0; i < textAppearings.Count - 1; i++)
        {
            textAppearings[i].OnStopTyping += () => OnStopPageTyping?.Invoke();
        }
        textAppearings[textAppearings.Count - 1].OnStopTyping += () =>
        {
            OnStopTyping?.Invoke();
        };
    }


    public void Type(List<string> fullTexts)
    {
        this.fullTexts = fullTexts;

        textIndex = 0;
        textAppearings[textIndex].Type(fullTexts[textIndex]);
        textIndex++;

        SetupPageControls();
    }
    public virtual void ShowPreviousPage()
    {
        textIndex--;
        textAppearings[textIndex].Stop();
        textIndex++;
    }
    public virtual void ShowNextPage()
    {
        textAppearings[textIndex].Type(fullTexts[textIndex]);
        textIndex++;

        SetupPageControls();
    }
    public virtual void Stop()
    {
        textAppearings.ForEach(x => x.Stop());
    }
    public void Reset()
    {
        textAppearings.ForEach(x => x.Reset());
    }

    protected void SetupPageControls()
    {
        if (textIndex == textAppearings.Count && !textAppearings[textAppearings.Count - 1].IsTyping)
        {
            nextPageControls?.ForEach(x => x.SetActive(false));
            stopPageControls?.ForEach(x => x.SetActive(true));

            //InvokeOnStopTyping();
        }
        else
        {
            nextPageControls?.ForEach(x => x.SetActive(true));
            stopPageControls?.ForEach(x => x.SetActive(false));
        }
    }
}
