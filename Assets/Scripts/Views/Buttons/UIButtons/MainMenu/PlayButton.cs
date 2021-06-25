using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryMenu
{
    public class PlayButton : StoryMenuButton
    {
        [SerializeField] private GameDayOrder gameDayOrder;
        [SerializeField] private FadeAnimation backgroundFadeAnimation;
        [SerializeField] private PauseButton pauseButton;
        [SerializeField] private SaveSlotsView saveSlotsView;


        private bool isPlaying;


        protected override void OnClick()
        {
            ShowSaveSlots();
        }

        private void ShowSaveSlots()
        {
            saveSlotsView.Show(new List<SaveSlot>(new SaveSlot[] { new SaveSlot(new System.DateTime(2021, 06, 25), null) }));
        }
    }
}