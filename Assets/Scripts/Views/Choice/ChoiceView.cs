using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoiceView : MonoBehaviour
{
    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private TextMeshProUGUI questionTextView;
    [SerializeField] private List<ChoiceButtonsSet> choiceButtonsSets;


    public void Show(ChoiceData choiceData)
    {
        fadeAnimation.Appear();

        questionTextView.text = choiceData.questionText;

        int setIndex = choiceData.choiceTexts.Count - 1;
        choiceButtonsSets.ForEach(x => x.Hide());
        choiceButtonsSets[setIndex].Show(choiceData.choiceTexts);
    }
    public void Hide()
    {
        fadeAnimation.Disappear();
    }
}
