using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ChoiceButton : UIButton, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI textView;
    [SerializeField] private RectTransform hoverShortBackground;
    [SerializeField] private RectTransform hoverLongBackground;
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
        ShowHover();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        pointer.SetActive(true);
        HideHover();
    }

    protected override void OnClick()
    {
        action?.Invoke();
        action = null;
    }

    private void ShowHover()
    {
        textView.ForceMeshUpdate();
        float textWidth = textView.textBounds.size.x;

        if (textWidth < hoverLongBackground.rect.width / 2.0f)
        {
            hoverLongBackground.gameObject.SetActive(false);
            hoverShortBackground.gameObject.SetActive(true);
        }
        else
        {
            hoverShortBackground.gameObject.SetActive(false);
            hoverLongBackground.gameObject.SetActive(true);
        }
    }
    private void HideHover()
    {
        hoverShortBackground.gameObject.SetActive(false);
        hoverLongBackground.gameObject.SetActive(false);
    }
}
