using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChoiceButtonsSet : MonoBehaviour, IHidable
{
    [SerializeField] private List<ChoiceButton> buttons;


    public void Show(List<Choice> choices, List<UnityAction> actions)
    {
        gameObject.SetActive(true);

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Show(choices[i], actions[i]);
        }
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
