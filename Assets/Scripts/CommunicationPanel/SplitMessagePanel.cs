using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static TextShowing;

public class SplitMessagePanel : CommunicationPanel, IResetable
{
    [SerializeField] private List<SplitTextAppearingSet> sets;


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
    public override void Hide()
    {
        base.Hide();

        sets.ForEach(x => x.Stop());
    }
    public override void Reset()
    {
        base.Reset();

        sets.ForEach(x => x.Reset());
    }
}
