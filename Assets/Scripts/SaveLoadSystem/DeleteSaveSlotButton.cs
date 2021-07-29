using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System;

public class DeleteSaveSlotButton : UIButton
{
    private const float DUMMY_MIN_WAITING_SECONDS = 0.55f;
    private const float DUMMY_MAX_WAITING_SECONDS = 1.75f;


    [SerializeField] SaveSlotButton saveSlotButton;
    [SerializeField] YesNoPermissionWindow permissionWindow;
    [SerializeField] EndlessLoadingCircleBar loadingBar;


    protected override void OnClick()
    {
        permissionWindow.Show(Act, null);
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
        loadingBar.Show();

        StartCoroutine(WaitForDeleteRoutine());
    }
    private IEnumerator WaitForDeleteRoutine()
    {
        // dummy waiting
        float waitingSeconds = UnityEngine.Random.Range(DUMMY_MIN_WAITING_SECONDS, DUMMY_MAX_WAITING_SECONDS);
        yield return new WaitForSeconds(waitingSeconds);

        loadingBar.Hide();
        saveSlotButton.Interactable = true;
        saveSlotButton.ShowEmpty();
    }
}
