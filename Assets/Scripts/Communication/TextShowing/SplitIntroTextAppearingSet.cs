using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitIntroTextAppearingSet : SplitTextAppearingSet
{
    [SerializeField] private List<FadeAnimation> betweenTextsObjects;
    [SerializeField] private Smoke smoke;


    private int betweenIndex;


    public override void Type(List<string> fullTexts)
    {
        betweenIndex = 0;
        betweenTextsObjects.ForEach(x => x.Disappear());

        base.Type(fullTexts);

        smoke.Appear(textAppearings[0].GetComponent<RectTransform>().position);
    }
    public override void ShowNextPage()
    {
        betweenTextsObjects[betweenIndex].Appear();
        betweenIndex++;

        smoke.Appear(textAppearings[textIndex].GetComponent<RectTransform>().position);
        textAppearings[textIndex].Type(fullTexts[textIndex]);
        textIndex++;

        SetupPageControls();
    }
    public override void Stop()
    {
        base.Stop();

        betweenTextsObjects.ForEach(x => x.Disappear());
    }
}
