using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsView : MonoBehaviour, IShowable
{
    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private AutoRectScroll autoRectScroll;


    public void Show()
    {
        fadeAnimation.Appear();
        autoRectScroll.Play();
    }
}
