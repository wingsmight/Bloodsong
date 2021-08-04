using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ChoiceButton : UIButton, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI textView;
    [SerializeField] private RectTransform[] hoverBackgrounds;
    [SerializeField] private GameObject pointer;
    [SerializeField] private float hoverDivideFactor;


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

    //TEST
    private void Update()
    {
        ShowHover();
    }
    private void ShowHover()
    {
        textView.ForceMeshUpdate();
        float textWidth = textView.textBounds.size.x;

        int hoverIndex = 1;
        float hoverWidth = hoverBackgrounds[hoverIndex].rect.width;
        while (hoverIndex < hoverBackgrounds.Length &&
            textWidth > (hoverWidth / hoverDivideFactor) + ((hoverIndex - 1) * hoverWidth / (float)hoverBackgrounds.Length / 2.0f))
        {
            hoverIndex++;
        }
        hoverIndex--;

        HideHover();
        hoverBackgrounds[hoverIndex].gameObject.SetActive(true);
    }
    private void HideHover()
    {
        for (int i = 0; i < hoverBackgrounds.Length; i++)
        {
            hoverBackgrounds[i].gameObject.SetActive(false);
        }
    }
}
