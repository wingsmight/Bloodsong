using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitIntroTextAppearingSet : SplitTextAppearingSet
{
    [SerializeField] private List<FadeAnimation> betweenTextsObjects;


    private int betweenIndex;


    public override void ShowNextPage()
    {
        if (betweenIndex == textIndex - 1)
        {
            betweenTextsObjects[betweenIndex].Appear();
            betweenIndex++;
        }
        else
        {
            textAppearings[textIndex].Type(fullTexts[textIndex]);
            textIndex++;

            SetupPageControls();
        }
    }
    public override void Stop()
    {
        base.Stop();

        betweenTextsObjects.ForEach(x => x.Disappear());
    }
}
