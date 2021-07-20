using System.Collections;
using System.Collections.Generic;
using StoryMenu;
using UnityEngine;

public class MenuView : MonoBehaviour, IShowable, IHidable
{
    [SerializeField] private FadeAnimation fadeAnimation;


    private void Start()
    {
        Show();
    }


    public void Show()
    {
        fadeAnimation.Appear();
    }
    public void Hide()
    {
        fadeAnimation.Disappear();
    }
}
