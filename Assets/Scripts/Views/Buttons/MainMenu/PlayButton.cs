using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private GameDayOrder gameDayOrder;
    [SerializeField] private FadeAnimation backgroundFadeAnimation;


    private bool isPlaying;


    public void OnClick()
    {
        if (!isPlaying)
        {
            isPlaying = true;

            gameDayOrder.StartDay();
        }
        backgroundFadeAnimation.Disappear();
    }
}
