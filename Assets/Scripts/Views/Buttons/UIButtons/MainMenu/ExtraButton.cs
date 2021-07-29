using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryMenu
{
    public class ExtraButton : StoryMenuButton
    {
        [SerializeField] private FadeAnimation menuButtonsFade;
        [SerializeField] private ExtraMenuView extraMenu;


        protected override void OnClick()
        {
            menuButtonsFade.Disappear();
            extraMenu.Show();
        }
    }
}