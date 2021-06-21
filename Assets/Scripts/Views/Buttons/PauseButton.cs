using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private const float backgroundAlpha = 0.8f;


    [SerializeField] private FadeAnimation menuFadeAnimation;


    public void OnClick()
    {
        menuFadeAnimation.Appear(backgroundAlpha);
    }
}
