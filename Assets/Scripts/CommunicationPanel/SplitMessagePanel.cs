using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static TextShowing;

public class SplitMessagePanel : CommunicationPanel
{
    [SerializeField] private List<SplitTextAppearingSet> sets;


    private UnityAction actionAfterStop;


    private void Awake()
    {
        sets.ForEach(x => x.gameObject.SetActive(false));
    }


    public void Show(List<string> fullTexts)
    {
        fadeAnimation.Appear();

        sets.ForEach(x => x.gameObject.SetActive(false));
        sets[fullTexts.Count - 1].gameObject.SetActive(true);
        sets[fullTexts.Count - 1].Type(fullTexts);
    }
}
