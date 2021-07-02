using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryMenu
{
    public class SettingsButton : StoryMenuButton
    {
        [SerializeField] private SettingsView settingsView;


        protected override void OnClick()
        {
            settingsView.Show();
        }
    }
}