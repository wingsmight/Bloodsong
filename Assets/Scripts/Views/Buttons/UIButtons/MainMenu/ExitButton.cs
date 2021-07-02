using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryMenu
{
    public class ExitButton : StoryMenuButton
    {
        protected override void OnClick()
        {
            Quit();
        }

        private void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}