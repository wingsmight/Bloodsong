using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideStoryMenuButton : HideButton
{
    [SerializeField] private GameDayControl gameDay;


    protected override void OnClick()
    {
        if (gameDay.IsRunning)
        {
            base.OnClick();
        }
    }
}
