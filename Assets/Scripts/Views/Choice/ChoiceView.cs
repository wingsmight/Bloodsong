using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ChoiceView : MonoBehaviour
{
    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private List<ChoiceButtonsSet> choiceButtonsSets;


    public void Show(ChoiceData choiceData, List<UnityAction> actions)
    {
        fadeAnimation.Appear();

        int setIndex = choiceData.choiceTexts.Count - 1;
        choiceButtonsSets.ForEach(x => x.Hide());
        choiceButtonsSets[setIndex].Show(choiceData.choiceTexts, actions);
    }
    public void Hide()
    {
        fadeAnimation.Disappear();
    }
}
