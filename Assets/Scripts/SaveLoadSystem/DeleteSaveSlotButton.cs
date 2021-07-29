using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System;

public class DeleteSaveSlotButton : UIButton
{
    [SerializeField] SaveSlotButton saveSlotButton;
    [SerializeField] YesNoPermissionWindow permissionWindow;


    protected override void OnClick()
    {
        permissionWindow.Show(Act, null);
    }

    private void Act()
    {
        GameFile.DeleteSaveSlot(saveSlotButton.Index);

        SetEnvironmentAfterDelete();
    }
    private void SetEnvironmentAfterDelete()
    {
        // TODO add little loading
        saveSlotButton.ShowEmpty();
    }
}
