using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryMenu
{
    public class PlayButton : StoryMenuButton
    {
        [SerializeField] private GameDayOrder gameDayOrder;
        [SerializeField] private FadeAnimation backgroundFadeAnimation;
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