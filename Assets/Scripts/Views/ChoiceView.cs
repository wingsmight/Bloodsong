using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoiceView : MonoBehaviour
{
    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private TextMeshProUGUI questionTextView;
    [SerializeField] private List<ChoiceButton> choiceButtons;


    public void Show(ChoiceData choiceData)
    {
        fadeAnimation.Appear();

        questionTextView.text = choiceData.questionText;

        for (int i = 0; i < choiceButtons.Count; i++)
        {
            choiceButtons[i].Show(choiceData.choiceTexts[i]);
        }
    }
    public void Hide()
    {
        fadeAnimation.Disappear();
    }


    public struct ChoiceData
    {
        public string questionText;
        public List<string> choiceTexts;
    }
}
