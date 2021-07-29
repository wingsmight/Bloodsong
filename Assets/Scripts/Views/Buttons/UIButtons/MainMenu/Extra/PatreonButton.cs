using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryMenu
{
    public class PatreonButton : StoryMenuButton
    {
        [SerializeField] private string link;


        protected override void OnClick()
        {
            Application.OpenURL(link);
        }
    }
}