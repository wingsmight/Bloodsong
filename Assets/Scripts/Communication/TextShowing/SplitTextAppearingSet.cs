using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TextShowing;

public class SplitTextAppearingSet : MonoBehaviour, IShowPaging
{
    [SerializeField] List<TextAppearing> textAppearings;


    public event eventDelegate OnStopPageTyping;
    public event eventDelegate OnStopTyping;
    public event eventDelegate OnStartTyping;

    private int textIndex;
    private List<string> fullTexts;


    private void Awake()
    {
        for (int i = 0; i < textAppearings.Count; i++)
        {
            textAppearings[i].OnStartTyping += OnStartTyping;
        }
        for (int i = 0; i < textAppearings.Count - 1; i++)
        {
            textAppearings[i].OnStopTyping += OnStopPageTyping;
        }
        textAppearings[textAppearings.Count - 1].OnStopTyping += OnStopTyping;
    }


    public void Type(List<string> fullTexts)
    {
        this.fullTexts = fullTexts;

        textIndex = 0;
        textAppearings[textIndex].Type(fullTexts[textIndex]);
        textIndex++;
    }
    public void ShowPreviousPage()
    {
        textIndex--;
        textAppearings[textIndex].Stop();
    }
    public void ShowNextPage()
    {
        textAppearings[textIndex].Type(fullTexts[textIndex]);
        textIndex++;
    }
    public void Stop()
    {
        textAppearings.ForEach(x => x.Stop());
    }
}
