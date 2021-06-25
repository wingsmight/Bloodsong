using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryMenu
{
    public class ExitButton : StoryMenuButton
    {
        protected override void OnClick()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}