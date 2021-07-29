using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryMenu
{
    public class SaveButton : StoryMenuButton
    {
        [SerializeField] private SaveLoadLauncher saveLoadLauncher;
        [SerializeField] private LoadingCircleBar loadingBar;


        protected override void OnClick()
        {
            loadingBar.Show();

            Storage.GeneralSettings.CurrentSaveSlot.lastExitDate = new DateTimeData(DateTime.Now);

            saveLoadLauncher.SaveDatas();
        }
    }
}