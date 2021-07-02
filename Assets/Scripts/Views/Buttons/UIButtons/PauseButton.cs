using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : UIButton
{
    private const float backgroundAlpha = 0.8f;


    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private MenuView menuView;


    public void Show()
    {
        fadeAnimation.Appear();
    }

    protected override void OnClick()
    {
        menuView.Show();
    }


    public FadeAnimation FadeAnimation => fadeAnimation;
}
