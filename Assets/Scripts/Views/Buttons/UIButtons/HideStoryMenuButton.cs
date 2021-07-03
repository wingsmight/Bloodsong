using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideStoryMenuButton : HideButton
{
    [SerializeField] private GameDayOrder gameDayOrder;


    protected override void OnClick()
    {
        if (gameDayOrder.IsRunning)
        {
            base.OnClick();
        }
    }
}
