using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitTextAppearingSet : MonoBehaviour, IShowPaging
{
    [SerializeField] List<TextAppearing> textAppearings;


    private int textIndex;
    private List<string> fullTexts;


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
