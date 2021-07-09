using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceButtonsSet : MonoBehaviour, IShowable<List<Choice>>, IHidable
{
    [SerializeField] private List<ChoiceButton> buttons;


    public void Show(List<Choice> choices)
    {
        gameObject.SetActive(true);

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Show(choices[i]);
        }
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
