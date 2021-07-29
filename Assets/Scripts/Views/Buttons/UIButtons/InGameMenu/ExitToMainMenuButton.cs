using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StoryMenu
{
    public class ExitToMainMenuButton : StoryMenuButton
    {
        private const string MAIN_MENU_SCENE_NAME = "MainMenu";


        protected override void OnClick()
        {
            Storage.GeneralSettings.CurrentSaveSlot.lastExitDate = new DateTimeData(DateTime.Now);

            SceneManager.LoadScene(MAIN_MENU_SCENE_NAME);
        }
    }
}