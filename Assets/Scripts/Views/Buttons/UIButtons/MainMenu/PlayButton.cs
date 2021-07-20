using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryMenu
{
    public class PlayButton : StoryMenuButton
    {
        [SerializeField] private SaveSlotsView saveSlotsView;


        private bool isPlaying;


        protected override void OnClick()
        {
            ShowSaveSlots();
        }

        private void ShowSaveSlots()
        {
            saveSlotsView.Show();
        }
    }
}