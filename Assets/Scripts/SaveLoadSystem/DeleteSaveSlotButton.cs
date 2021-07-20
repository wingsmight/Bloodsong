using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System;

public class DeleteSaveSlotButton : UIButton
{
    [SerializeField] SaveSlotButton saveSlotButton;


    protected override void OnClick()
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
