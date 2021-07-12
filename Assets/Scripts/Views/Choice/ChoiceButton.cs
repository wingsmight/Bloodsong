using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ChoiceButton : UIButton, IPointerEnterHandler, IPointerExitHandler
{
    // [SerializeField] private ChoiceView choiceView;
    [Space(12)]
    [SerializeField] private TextMeshProUGUI textView;
    [SerializeField] private GameObject hoverBackground;
    [SerializeField] private GameObject pointer;


    private UnityAction action;


    public void Show(Choice choice, UnityAction action)
    {
        textView.text = choice.text;
        this.action = action;

        OnPointerExit(null);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        pointer.SetActive(false);
        hoverBackground.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        pointer.SetActive(true);
        hoverBackground.SetActive(false);
    }


    protected override void OnClick()
    {
        action?.Invoke();
        action = null;
    }
}
