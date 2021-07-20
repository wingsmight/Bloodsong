using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ChoiceButton : UIButton, IPointerEnterHandler, IPointerExitHandler
{
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
        PlacePointer();
        hoverBackground.SetActive(false);
    }

    protected override void OnClick()
    {
        action?.Invoke();
        action = null;
    }

    private void PlacePointer()
    {
        textView.ForceMeshUpdate();
        pointer.transform.localPosition = new Vector2(-textView.textBounds.size.x / 2.0f, pointer.transform.localPosition.y);
    }
}
