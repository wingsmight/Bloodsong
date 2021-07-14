using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryMenu
{
    public class ExitToMainMenuButton : StoryMenuButton
    {
        [SerializeField] private MenuView menuView;


        protected override void OnClick()
        {
            Storage.GetData<PlayerPreferences>().lastExitDate = new DateTimeData(DateTime.Now);

            menuView.Show();
        }
    }
}