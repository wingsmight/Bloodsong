using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryMenu
{
    public class ExitToMainMenuButton : StoryMenuButton
    {
        [SerializeField] private MenuView menuView;
        [SerializeField] private SaveButton saveButton;


        protected override void OnClick()
        {
            menuView.Show();
        }
    }
}