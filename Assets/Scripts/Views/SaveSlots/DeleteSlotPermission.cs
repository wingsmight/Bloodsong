using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteSlotPermission : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Button yesButton;
    [SerializeField] private List<Button> noButtons;


    private Action actionAfterYes;
    private Action actionAfterNo;


    private void Awake()
    {
        yesButton.enabled = false;
    }


    public void Show(Action actionAfterYes, Action actionAfterNo)
    {
        Show();

        this.actionAfterYes = actionAfterYes;
        this.actionAfterNo = actionAfterNo;

        yesButton.onClick.AddListener(OnButtonYesClicked);
        noButtons.ForEach(x => x.onClick.AddListener(OnButtonNoClicked));
    }
    public void Hide()
    {
        yesButton.enabled = false;
        if (animator.GetCurrentName(0) == "Appear")
        {
            animator.Play("Disappear", 0, 0.0f);
        }
    }

    private void Show()
    {
        animator.Play("Appear", 0, 0.0f);
        yesButton.enabled = true;
    }

    private void OnButtonYesClicked()
    {
        Hide();

        actionAfterYes?.Invoke();

        yesButton.onClick.RemoveListener(OnButtonYesClicked);
    }
    private void OnButtonNoClicked()
    {
        Hide();

        actionAfterNo?.Invoke();

        noButtons.ForEach(x => x.onClick.RemoveListener(OnButtonNoClicked));
    }
}
