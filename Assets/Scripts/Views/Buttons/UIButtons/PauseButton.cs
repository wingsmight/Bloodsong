using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : UIButton
{
    private const float backgroundAlpha = 0.8f;


    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private FadeAnimation menuFadeAnimation;


    protected override void OnClick()
    {
        menuFadeAnimation.Appear(backgroundAlpha);
    }


    public FadeAnimation FadeAnimation => fadeAnimation;
}
