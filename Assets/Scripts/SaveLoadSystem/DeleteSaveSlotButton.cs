using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System;

public class DeleteSaveSlotButton : UIButton
{
    [SerializeField] FadeAnimation fadeAnimation;
    [SerializeField] SaveSlotButton saveSlotButton;
    [SerializeField] DeleteSlotPermission permissionOverlay;
    [Space(12)]
    [SerializeField] float frameFadeSpeed;
    [SerializeField] Image loadingFrame;
    [SerializeField] FadeAnimation emptySlot;
    [SerializeField] FadeAnimation filledSlot;


    protected override void OnClick()
    {
        permissionOverlay.Show(Act, null);
    }

    private void Act()
    {
        GameFile.DeleteSaveSlot(saveSlotButton.Index);
        saveSlotButton.HideDeleteButton();
        saveSlotButton.Interactable = false;

        SetEnvironmentAfterDelete();
    }
    private void SetEnvironmentAfterDelete()
    {
        StartCoroutine(WaitForDeleteRoutine());
    }
    private IEnumerator WaitForDeleteRoutine()
    {
        loadingFrame.fillAmount = 1.0f;
        emptySlot.Appear();
        filledSlot.Disappear();
        fadeAnimation.Disappear();

        while (loadingFrame.fillAmount > 0.0f)
        {
            loadingFrame.fillAmount -= frameFadeSpeed * Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        saveSlotButton.Interactable = true;
        saveSlotButton.ShowEmpty();
    }
}
