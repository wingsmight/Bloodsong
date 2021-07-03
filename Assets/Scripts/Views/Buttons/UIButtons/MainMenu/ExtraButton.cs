using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryMenu
{
    public class ExtraButton : StoryMenuButton
    {
        [SerializeField] private ExtraMenuView extraMenu;


        protected override void OnClick()
        {
            extraMenu.Show();
        }
    }
}