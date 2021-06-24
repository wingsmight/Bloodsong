using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : UIButton
{
    [SerializeField] private GameDayOrder gameDayOrder;
    [SerializeField] private FadeAnimation backgroundFadeAnimation;
    [SerializeField] private PauseButton pauseButton;


    private bool isPlaying;


    protected override void OnClick()
    {
        if (!isPlaying)
        {
            isPlaying = true;

            gameDayOrder.StartDay();

            pauseButton.FadeAnimation.Appear();
        }
        backgroundFadeAnimation.Disappear();
    }
}