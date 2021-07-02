using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuView : MonoBehaviour, IShowable, IHidable
{
    [SerializeField] private FadeAnimation fadeAnimation;
    [Space(12)]
    [SerializeField] private PauseButton pauseButton;


    public void Show()
    {
        fadeAnimation.Appear();
    }
    public void Hide()
    {
        fadeAnimation.Disappear();

        pauseButton.Show();
    }
}
